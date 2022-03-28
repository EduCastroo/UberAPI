using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UberAPI.Model;

namespace UberAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        //apenas fazer uma leitura
        public readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
           await _context.SaveChangesAsync(); //await => espera até ser processado e executa
            return user;
        }

        public async Task Delete(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id); //procura de forma assincrona o registro no banco
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> Get()
        {
          return await  _context.Users.ToListAsync();
        }

        public async Task<User> Get(int id) //buscar pelo id especifico
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
