using System.Collections.Generic;
using System.Threading.Tasks;
using CloudAuditionApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudAuditionApi.DatabaseService
{
    public interface IMessageDbService 
    {
        Task<List<Message>> GetAllAsync();

        Task<Message> FindAsync(long id);

        Task CreateAsync(Message message);

        Task UpdateAsync(Message message);

        Task DeleteAsync(Message message);
    }

    public class MessageDbService : IMessageDbService
    {
        CloudAuditionApiContext _context;

        public MessageDbService(CloudAuditionApiContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetAllAsync() => await _context.Messages.ToListAsync();

        public async Task<Message> FindAsync(long id) => await _context.Messages.FindAsync(id);

        public async Task CreateAsync(Message message) 
        {
            _context.Messages.Add(message);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Message message) 
        {
            _context.Entry(message).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}