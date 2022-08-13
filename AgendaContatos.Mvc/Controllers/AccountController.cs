using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();

                    if (usuarioRepository.GetByEmailESenha(model.Email, model.Senha) != null)
                    {
                        return RedirectToAction("Consulta", "Contatos");
                    }
                    else
                    {
                        TempData["Message"] = $"Acesso negado. Usuário inválido.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar: {e.Message}";
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();

                    if (usuarioRepository.GetByEmail(model.Email) != null)
                    {
                        TempData["Mensagem"] = $"O email {model.Email} já está cadastrado para outro usuário. Tente outro e-mail.";
                    }
                    else
                    {
                        var usuario = new Usuario();

                        usuario.IdUsuario = Guid.NewGuid();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = model.Senha;
                        usuario.DataCadastro = DateTime.Now;

                        usuarioRepository.Create(usuario);

                        TempData["Mensagem"] = $"Parabéns {usuario.Nome}, sua conta foi cadastrada com sucesso!";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar: {e.Message}";
                }
            }

            return View();
        }

        public IActionResult PasswordRecover()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordRecover(AccountPasswordRecoverModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmail(model.Email);

                    if (usuario != null)
                    {
                        TempData["Mensagem"] = $"Olá {usuario.Nome}, você receberá um e-mail para cadastrar uma nova senha.";
                    }
                    else
                    {
                        TempData["Mensagem"] = $"O e-mail informado não existe no sistema, por favor verifique.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao recuperar senha: {e.Message}";
                }
            }
            return View();
        }
    }
}
