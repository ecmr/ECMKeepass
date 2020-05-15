﻿using ECMKeepass.Modelo;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ECMKeepass.Banco
{

    class Database
    {
        private SQLiteConnection _conexao;

        public Database()
        {
            var dep = DependencyService.Get<ICaminho>();
            _conexao = new SQLiteConnection(dep.ObterCaminho("database.sqlite"));
            _conexao.CreateTable<keepass>();
            _conexao.CreateTable<UserAcess>();
            _conexao.CreateTable<KeepGroup>();
        }

        #region Grupo
        public List<KeepGroup> Listar()
        {
            return _conexao.Table<KeepGroup>().ToList();
        }
        public List<KeepGroup> PesquisarGroupPorParavra(string palavra)
        {
            return _conexao.Table<KeepGroup>().Where(k => k.GrupoNome.Contains(palavra)).ToList();
        }
        public KeepGroup ObterKeeGroupPorId(int id)
        {
            return _conexao.Table<KeepGroup>().Where(a => a.Id == id).FirstOrDefault();
        }
        public void CadastroGrupo(KeepGroup kg)
        {
            _conexao.Insert(kg);
        }
        public void AtualizacaoGrupo(KeepGroup kg)
        {
            _conexao.Update(kg);
        }
        public void ExclusaoGrupo(KeepGroup kg)
        {
            _conexao.Delete(kg);
        }



        #endregion

        #region Senhas
        public List<keepass> Consultar()
        {
            return _conexao.Table<keepass>().ToList();
        }
        public List<keepass> Pesquisar(string palavra)
        {
            return _conexao.Table<keepass>().Where(k => k.Titulo.Contains(palavra)).ToList();
        }
        public keepass ObterKeePorId(int id)
        {
            return _conexao.Table<keepass>().Where(a => a.Id == id).FirstOrDefault();
        }
        public void Cadastro(keepass kee)
        {
            _conexao.Insert(kee);
        }
        public void Atualizacao(keepass kee)
        {
            _conexao.Update(kee);
        }
        public void Exclusao(keepass kee)
        {
            _conexao.Delete(kee);
        }
        #endregion

        #region Usuario
        public List<UserAcess> ConsultarPrimeiroAcesso()
        {
            return _conexao.Table<UserAcess>().ToList();
        }

        public bool PequisaUsuario(UserAcess useracess)
        {
            UserAcess user = _conexao.Table<UserAcess>().Where(ua => ua.Usuario.Contains(useracess.Usuario)).Where(ua => ua.Senha.Contains(useracess.Senha)).FirstOrDefault();
            if (user != null)
                return true;
            else
                return false;
        }

        public void CadastroPrimeiroACesso(UserAcess user)
        {
            _conexao.Insert(user);
        }
        #endregion




    }
}
