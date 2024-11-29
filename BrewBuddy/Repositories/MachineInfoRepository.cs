﻿using BrewBuddy.Interface;
using BrewBuddy.Migrations;
using BrewBuddy.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewBuddy.Repositories
{
    public class MachineInfoRepository : IRepository<MachineInfo>
    {
        private readonly BrewBuddyContext _context;

        //vi laver en konstruktør som tager brewbuddycontext som parameter 
        public MachineInfoRepository(BrewBuddyContext context)
        {
            _context = context;
        }

        public void Add(MachineInfo machineInfo)
        {
            _context.MachineInfos.Add(machineInfo); //her kan vi tilføje en ny maskine 
            _context.SaveChanges(); //denne er vigtig for det er den der sørger for at gemme den nye maskine 
        }


        public void Delete(int Id)
        {
            var machineInfo = _context.MachineInfos.Find(Id);

            // If the machine exists, remove it
            if (machineInfo != null)
            {
                _context.MachineInfos.Remove(machineInfo);
                _context.SaveChanges(); // Save changes to commit the deletion
            }
        }

        public List<MachineInfo> GetAll()
        {
            return _context.MachineInfos.ToList();
        }

        public MachineInfo GetAllById(int Id)
        {
            return _context.MachineInfos.FirstOrDefault(m => m.InfoId == Id);
        }

        public Task<MachineInfo> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(MachineInfo updateInfo)
        {
            _context.MachineInfos.Update(updateInfo);
            _context.SaveChanges();
        }

        public Task UpdateAsync(MachineInfo entity)
        {
            throw new NotImplementedException();
        }
    }
}