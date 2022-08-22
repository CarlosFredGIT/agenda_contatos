using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgendaContatos.Mvc.Controllers
{
    [Authorize]
    public class ContatosController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContatosCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var authenticationModel = ObterUsuarioAutenticado();

                    var contato = new Contato();

                    contato.IdContato = Guid.NewGuid();
                    contato.Nome = model.Nome;
                    contato.Email = model.Email;
                    contato.Telefone = model.Telefone;
                    contato.DataNascimento = DateTime.Parse(model.DataNascimento);
                    contato.IdUsuario = authenticationModel.IdUsuario;

                    var contatoRepository = new ContatoRepository();
                    contatoRepository.Create(contato);

                    TempData["Mensagem"] = $@"Contato {contato.Nome}, cadastrado com sucesso!";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $@"Falha ao cadastrar contato: {e.Message}";
                }
            }

            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Consulta(ContatosConsultaModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }

        public IActionResult Edicao()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edicao(ContatosEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $@"Falha: {e.Message}";
                }
            }

            return View();
        }

        public AuthenticationModel ObterUsuarioAutenticado()
        {
            var json = User.Identity.Name;
            return JsonConvert.DeserializeObject<AuthenticationModel>(json);    
        }
    }
}
