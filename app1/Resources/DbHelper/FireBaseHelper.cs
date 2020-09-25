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
    class FireBaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://jellystone-at.firebaseio.com/");

        public async Task<List<User>> GetAllUsers()
        {
            return (await firebase
                .Child("Users")
                .OnceAsync<User>()).Select(item => new User
                {
                    Email = item.Object.Email,
                    Senha = item.Object.Senha,
                    Nome = item.Object.Nome,
                    Sobrenome = item.Object.Sobrenome
                }).ToList();
        }

        public async Task AddUser(string email, string senha, string nome, string sobrenome)
        {
            await firebase
                .Child("Users")
                .PostAsync(new User() { Email = email, Senha = senha, Nome = nome, Sobrenome = sobrenome });
        }

        public async Task<User> GetUser(string email)
        {
            var allusers = await GetAllUsers();
            await firebase
                .Child("Users")
                .OnceAsync<User>();
            return allusers.Where(a => a.Email == email).FirstOrDefault();
        }
        public async Task<User> GetSenhaExiste(string senha)
        {
            var alluser = await GetAllUsers();
            await firebase
                .Child("Users")
                .OnceAsync<User>();
            return alluser.Where(a => a.Senha == senha).FirstOrDefault();
        }
    }
}