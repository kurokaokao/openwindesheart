﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using WindesHeartApp.Data.Interfaces;
using WindesHeartApp.Models;


namespace WindesHeartApp.Data.Repository
{
    public class SleepRepository : ISleepRepository
    {
        private readonly DatabaseContext _databaseContext;
        public SleepRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }

        public async Task<IEnumerable<Sleep>> GetAllAsync()
        {
            try
            {
                var sleep = await _databaseContext.Sleep.ToListAsync();
                return sleep.OrderBy(x => x.DateTime).ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> AddAsync(Sleep sleep)
        {
            try
            {
                var tracking = await _databaseContext.Sleep.AddAsync(sleep);
                return tracking.State == EntityState.Added;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public void RemoveAll()
        {
            try
            {
                _databaseContext.Sleep.Clear<Sleep>();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Could not delete sleep entries: " + e);
            }
        }

        public async Task<IEnumerable<Sleep>> SleepByQueryAsync(Func<Sleep, bool> predicate)
        {
            try
            {
                var sleep = await _databaseContext.Sleep.ToListAsync();
                return sleep.Where(predicate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async void SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}