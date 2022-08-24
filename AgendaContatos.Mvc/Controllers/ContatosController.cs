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
            var lista = new List<ContatosConsultaModel>();

            try
            {
                var authenticationModel = ObterUsuarioAutenticado();
                var contatoRepository = new ContatoRepository();
                var contatos = contatoRepository.GetByUsuario(authenticationModel.IdUsuario);

                foreach (var item in contatos)
                {
                    var model = new ContatosConsultaModel();

                    model.IdContato = item.IdContato;
                    model.Nome = item.Nome;
                    model.Telefone = item.Telefone;
                    model.Email = item.Email;
                    model.DataNascimento = item.DataNascimento.ToString("dd/MM/yyyy");
                    model.Idade = ObterIdade(item.DataNascimento);

                    lista.Add(model);
                }
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = $@"Falha: {e.Message}";
            }

            return View(lista);
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

        public int ObterIdade(DateTime dataNascimento)
        {
            var idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
                idade--;

            return idade;
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var contatoRepository = new ContatoRepository();
                var contato = new Contato();

                contato.IdContato = id;

                contatoRepository.Delete(contato);

                TempData["Mensagem"] = $@"Contato excluído com sucesso.";
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = $@"Falha ao excluir: {e.Message}";
            }
            
            return RedirectToAction("Consulta");
        }
    }
}
