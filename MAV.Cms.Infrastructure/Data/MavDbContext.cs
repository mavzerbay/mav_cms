using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Utilities;
using MAV.Cms.Domain.Entities;
using MAV.Cms.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MAV.Cms.Infrastructure.Data
{
    public class MavDbContext : IdentityDbContext<MavUser, MavRole, Guid,
      IdentityUserClaim<Guid>, MavUserRole, IdentityUserLogin<Guid>,
      IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        private IHttpContextAccessor _httpContext => new HttpContextAccessor();
        public string TenantId { get; set; }
        //private readonly ITenantService _tenantService;
        public MavDbContext(DbContextOptions<MavDbContext> options) : base(options)
        {
            //_tenantService = tenantService;
            //TenantId = _tenantService.GetTenant()?.TID;
        }

        #region Identity

        public DbSet<MavUser> MavUsers { get; set; }
        public DbSet<MavRole> MavRoles { get; set; }
        public DbSet<MavUserRole> MavUserRoles { get; set; }

        #endregion
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryTrans> CategoryTrans { get; set; }
        public DbSet<PageComment> PageComment { get; set; }
        public DbSet<CustomVar> CustomVar { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }
        public DbSet<GeneralSettingsTrans> GeneralSettingsTrans { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuTrans> MenuTrans { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<PageTrans> PageTrans { get; set; }
        public DbSet<Slide> Slide { get; set; }
        public DbSet<SlideMedia> SlideMedia { get; set; }
        public DbSet<SupportTicket> SupportTicket { get; set; }
        public DbSet<Translate> Translate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected virtual void OnBeforeSaving()
        {
            bool isAuthenticated = false;
            Guid? userId = null;
            string localIp = null;
            string remoteIp = null;

            if (_httpContext.HttpContext != null)
            {
                isAuthenticated = _httpContext.HttpContext.User.Identity.IsAuthenticated;
                userId = _httpContext.HttpContext.User.GetUserId();
                localIp = NetworkUtils.GetLocalIp();
                remoteIp = NetworkUtils.GetRemoteIp(_httpContext);
            }


            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {

                switch (entry.State)
                {
                    case EntityState.Added:
                        if (isAuthenticated && userId.HasValue)
                            entry.Entity.CreatedById = userId.Value;

                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedLocalIp = localIp;
                        entry.Entity.CreatedRemoteIp = remoteIp;
                        break;

                    case EntityState.Modified:
                        if (isAuthenticated && userId.HasValue)
                            entry.Entity.UpdatedById = userId.Value;

                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        entry.Entity.UpdatedLocalIp = localIp;
                        entry.Entity.UpdatedRemoteIp = remoteIp;

                        if (entry.Entity.isSoftDelete.HasValue && entry.Entity.isSoftDelete.Value)
                        {
                            if (isAuthenticated && userId.HasValue)
                                entry.Entity.DeletedById = userId.Value;

                            entry.Entity.DeletedDate = DateTime.UtcNow;
                            entry.Entity.DeletedLocalIp = localIp;
                            entry.Entity.DeletedRemoteIp = remoteIp;
                        }
                        break;

                    case EntityState.Deleted:
                        if (isAuthenticated && userId.HasValue)
                            entry.Entity.DeletedById = userId.Value;

                        entry.Entity.DeletedDate = DateTime.UtcNow;
                        entry.Entity.DeletedLocalIp = localIp;
                        entry.Entity.DeletedRemoteIp = remoteIp;
                        break;
                }
            }
        }
    }
}
