using Domains.Entities;

namespace Domains.IServices {
    public interface IToken {
        string CreateToken (User user);
    }
}