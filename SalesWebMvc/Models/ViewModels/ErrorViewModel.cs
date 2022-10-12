using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }//Id interno da requisi��o
        public string Message { get; set; }
        //metodo para testar se o id existe
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}