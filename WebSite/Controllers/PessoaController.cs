using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class PessoaController : Controller
    {

        //
        // GET: /Pessoa/
        public ActionResult Index()
        {
            ViewBag.Mensagem = "Minha primeira View";
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PessoaModels model)
        {
            ModelState.Remove("Codigo");

            List<PessoaModels> lista = new List<PessoaModels>();

            if (ModelState.IsValid)
            {
                if (Session["ListaPessoas"] != null)
                {
                    lista.AddRange((List<PessoaModels>)Session["ListaPessoas"]);
                }

                model.Codigo = lista.Count + 1;

                lista.Add(model);
                Session["ListaPessoas"] = lista;
            }
            else
                return View(model);
            return View("List", lista);
        }

        public ActionResult List()
        {
            if (Session["ListaPessoas"] != null)
            {
                var model = (List<PessoaModels>)Session["ListaPessoas"];
                return View(model);
            }

            return View(new List<PessoaModels>());
        }

        public ActionResult Edit(int id)
        {
            //Recuperar o objeto com o id
            //Enviar o objeto encontrado para a View de Edição

            if (((List<PessoaModels>)Session["ListaPessoas"]).Where(p => p.Codigo == id).Any())
            {
                var model = ((List<PessoaModels>)Session["ListaPessoas"])
                    .Where(p => p.Codigo == id).FirstOrDefault();

                return View("Create", model);
            }

            return View("Create", new PessoaModels());
        }

        [HttpPost]
        public ActionResult Edit(PessoaModels model)
        {
            //Recuperar o objeto com o id
            //Alterar objeto com o objeto do parametro
            //Aplicar/Salvar objeto alterado na fonte de dados

            if (ModelState.IsValid)
            {
                if (Session["ListaPessoas"] != null)
                {
                    if (((List<PessoaModels>)Session["ListaPessoas"])
                        .Where(p => p.Codigo == model.Codigo).Any())
                    {
                        var modelBase = ((List<PessoaModels>)Session["ListaPessoas"])
                            .Where(p => p.Codigo == model.Codigo).FirstOrDefault();

                        //Atualiza seu registro com o model enviado por parametro...
                        ((List<PessoaModels>)Session["ListaPessoas"])[model.Codigo - 1] = model;
                    }

                    var lista = (List<PessoaModels>)Session["ListaPessoas"];
                    return View("List", lista);
                }
                else
                    return View(new List<PessoaModels>());
            }
            else
                return View("Create", model);
        }

        public ActionResult Delete(int id)
        {
            if (Session["ListaPessoas"] != null && id > 0)
            {
                if (((List<PessoaModels>)Session["ListaPessoas"])
                    .Where(p => p.Codigo == id).Any())
                {
                    var modelBase = ((List<PessoaModels>)Session["ListaPessoas"])
                        .Where(p => p.Codigo == id).FirstOrDefault();

                    var lista = ((List<PessoaModels>)Session["ListaPessoas"]);
                    lista.Remove(modelBase);

                    //Atualiza seu registro com o model enviado por parametro...
                    Session["ListaPessoas"] = lista;
                    return View("List", lista);
                }
            }

            return View("List", new List<PessoaModels>());
        }
    }

}
