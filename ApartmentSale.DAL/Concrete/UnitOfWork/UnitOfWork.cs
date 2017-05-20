namespace ApartmentSale.DAL.Concrete.UnitOfWork
{
    using System;
    using Repositories;

    public class UnitOfWork : IDisposable
    {
        private AppSaleContext _context = new AppSaleContext();
        private GenericRepository<User> _userRepository;
        private GenericRepository<Advertisement> _advertisementRepository;
        private bool _disposed = false;

        public GenericRepository<User> UserRepository =>
            _userRepository ?? (_userRepository = new GenericRepository<User>(_context));
       
        public GenericRepository<Advertisement> AdvertisementRepository =>
            _advertisementRepository ?? (_advertisementRepository = new GenericRepository<Advertisement>(_context));
     
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this._disposed = true;
        }
    }
}
