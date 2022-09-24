using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        //readonly prevents the dependency being changed
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            //utilizando o Include para fazer um join entre Seller e Department para exibir na tela Details
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
