using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using VanillaCare.Models;

namespace VanillaCare.Services
{
    public class TaskTuteurServices
    {
        readonly SQLiteAsyncConnection _database;

        public TaskTuteurServices(String  databaseName)
        {
            _database = new SQLiteAsyncConnection(databaseName);
            _database.CreateTableAsync<TaskTuteur>();
        }

        public Task<List<TaskTuteur>> GetAllItems()
        {
            return _database.Table<TaskTuteur>().OrderByDescending(x=>x.Name).ToListAsync();
        }

        public Task<int> SaveItemAsync(TaskTuteur tasktuteur)
        {
            //-- détruire ancien --
            var delta = _database.Table<TaskTuteur>().FirstOrDefaultAsync(x => x.Name == tasktuteur.Name);
            _database.DeleteAsync(delta);
            //-- ajouter nouvelle entrée --
            return _database.InsertAsync(tasktuteur);
        }

        public async Task DeleteAllItem()
        {
            try
            {
                await _database.DeleteAllAsync<TaskTuteur>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        public async Task<TaskTuteur> LoadItemAsyncByName(string _Name)
        {
            try 
            { 
                var delta = await _database.Table<TaskTuteur>().FirstOrDefaultAsync(x=>x.Name == _Name);
                return delta;
            }
            catch(Exception ex) 
            {
                Debug.WriteLine($"{ex.Message}");
            }

            return new TaskTuteur();
        }

        public async Task<TaskTuteur> GetItemAsyncByID(int _ID)
        {
            try
            {
                var delta = await _database.Table<TaskTuteur>().FirstOrDefaultAsync(x => x.Id == _ID);
                return delta;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(""+ex);
            }

            return new TaskTuteur();
        }

    }
}
