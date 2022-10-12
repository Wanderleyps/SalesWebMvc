using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    /**
     * Por padrão, quando se cria uma ação no ASP.NET Core MVC, esta ação vai corresponder ao método GET do HTTP,
     * portanto não precisa por uma anotation para informar as ações de GET.
     */
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        //criando um contrutor para poder aplicar a injeção de dependencia
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            //passando a lista para a view para ser gerada no carregamento
            return View(list);//gera um IActionResult contendo a lista e encaminha para a view
        }
        //ação de GET
        public IActionResult Create()
        {
            //na inicialização da tela jé serão carregados os objs de Departmens
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
           //Passando o objeto pr view
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//previne que a aplicação sofra ataques CSRF(quando alguem aproveita a sessão de atenticação para enviar dados maliciosos
        public IActionResult Create(Seller seller)//recebe o obj da view
        {
            if(!ModelState.IsValid)
            {
                return View(seller);
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provaded"});
            }
            var obj = _sellerService.FindById(id.Value);//como o parametro foi colocado como opcional, é necessário utilizar o .Valeu
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(seller);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" }); ;//o id do vendedor que está atualizando não pode ser diferente do id da url da requisição
            }   
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier//macete para pegar o id interno da requisição
            };
            return View(viewModel);//manda o objeto para a view
        }
    }
}
