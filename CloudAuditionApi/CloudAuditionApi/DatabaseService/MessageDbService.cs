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
    }

    public class MessageDbService : IMessageDbService
    {
        MessageContext _context;

        public MessageDbService(MessageContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetAllAsync() 
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> FindAsync(long id)
        {
            return await _context.Messages.FindAsync(id);
        }
    }

    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}