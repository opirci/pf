using Pars.Core;
using Pars.Util.Puma.Data.Repository;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Business
{
    public class UserBusiness : IUserBusiness
    {
        readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public UserBusiness() : this(Container.Instance.Resolve<IUserRepository>())
        {

        }
        public ITQueryable<User> Users => _userRepository.Users;

        //public List<LookupItem> SearchUser(string username)
        //{
        //    return _userRepository.GetUsers(new TextSearch(username)).AsLookup(r =>  r.UserRef, r => r.Username).ToList();
        //}

        //public List<User> GetUsers()
        //{
        //    return _userRepository.GetUsers().ToList();
        //}
    }
}
