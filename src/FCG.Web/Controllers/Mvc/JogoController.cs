using FCG.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Web.Controllers.Mvc
{
    public class JogoController : Controller
    {
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View(); // Retorna a view do formulário
        }

        [HttpGet]

        //Não irá retornar um view, alterar o return  função
        public IActionResult Cadastrar(Jogo jogo)
        {
            // Aqui você pode salvar no banco de dados
            Console.WriteLine($"Recebido: {jogo.Nome}, {jogo.Genero}, {jogo.Plataforma}, {jogo.Descricao}");

            // Redirecionar ou retornar mensagem de sucesso
            return RedirectToAction("Privacy"); // ou return View("Sucesso");
        }
    }
}

