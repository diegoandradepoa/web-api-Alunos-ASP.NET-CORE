﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{   
    [EnableCors("*","*","*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                Aluno aluno = new Aluno();
                return Ok(aluno.ListarAluno());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
           
        }

        // GET: api/Aluno/5
        [HttpGet]
        [Route("Recuperar/{id:int}/{nome}/{sobrenome=andrade}")]
        public Aluno Get(int id, string nome, string sobrenome)
        {
            Aluno aluno = new Aluno();

            return aluno.ListarAluno().Where(x => x.id ==id).FirstOrDefault();
        }

        [HttpGet]
        [Route(@"RecuperarPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(5)}")]
        public IHttpActionResult Recuperar(string data, string nome)
        {
            try
            {
                Aluno aluno = new Aluno();

                IEnumerable<Aluno> alunos = aluno.ListarAluno().Where(x => x.data == data || x.nome == nome);

                if (!alunos.Any())
                        return NotFound();

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
           
        }

        // POST: api/Aluno
        public List<Aluno> Post(Aluno aluno)
        {
            Aluno _aluno = new Aluno();

            _aluno.Inserir(aluno);

            return _aluno.ListarAluno();
        }

        // PUT: api/Aluno/5
        public Aluno Put(int id, [FromBody]Aluno aluno)
        {
            Aluno _aluno = new Aluno();

            return _aluno.Atualizar(id, aluno);
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
            Aluno _aluno = new Aluno();

            _aluno.Deletar(id);
        }
    }
}
