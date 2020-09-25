using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using app1.Resources.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace app1.Resources.DbHelper
{
    class FireBaseProjetosHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://jellystone-at.firebaseio.com/");

        public async Task<List<Projetos>> GetAllProjetos()
        {
            return (await firebase
                .Child("Projetos")
                .OnceAsync<Projetos>()).Select(item => new Projetos
                {
                    Nome = item.Object.Nome,
                    Categoria = item.Object.Categoria,
                    Data_inicio = item.Object.Data_finalizacao,
                    Data_finalizacao = item.Object.Data_finalizacao,
                    Organizadores = item.Object.Organizadores,
                    Descricao = item.Object.Descricao
                }).ToList();
        }

        public async Task AddProjetos(string nome, string data_inicio, string data_final, string organizadores, string descricao, string categoria)
        {
            await firebase
                .Child("Projetos")
                .PostAsync(new Projetos() { Nome = nome, Categoria = categoria, Data_inicio = data_inicio, Data_finalizacao = data_final, Organizadores = organizadores, Descricao = descricao });
        }

        public async Task<Projetos> GetProjetos(string nome)
        {
            var allProjetos = await GetAllProjetos();
            await firebase
                .Child("Projetos")
                .OnceAsync<Projetos>();
            return allProjetos.Where(a => a.Nome == nome).FirstOrDefault();
        }
    }
}