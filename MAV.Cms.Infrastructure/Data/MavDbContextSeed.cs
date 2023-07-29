using MAV.Cms.Domain.Entities;
using MAV.Cms.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAV.Cms.Infrastructure.Data
{
    public class MavDbContextSeed
    {
        public static async Task SeedAsync(MavDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                bool saveChanges = false;
                if (!context.Language.Any())
                {
                    await context.Language.AddRangeAsync(GetLanguages());
                    saveChanges = true;
                }
                if (!context.CustomVar.Any())
                {
                    await context.CustomVar.AddRangeAsync(GetCustomVars());
                    saveChanges = true;
                    await context.SaveChangesAsync(System.Threading.CancellationToken.None);
                }
                if (!context.Page.Any())
                {
                    await context.Page.AddRangeAsync(GetPages());
                    saveChanges = true;
                    await context.SaveChangesAsync(System.Threading.CancellationToken.None);
                }
                if (!context.Slide.Any())
                {
                    await context.Slide.AddRangeAsync(GetSlides());
                    saveChanges = true;
                    await context.SaveChangesAsync(System.Threading.CancellationToken.None);
                }
                if (!context.Menu.Any())
                {
                    await context.Menu.AddRangeAsync(GetMenus());
                    saveChanges = true;
                }
                if (!context.GeneralSettings.Any())
                {
                    await context.GeneralSettings.AddRangeAsync(GetGeneralSettings());
                    saveChanges = true;
                }
                if (!context.Translate.Any())
                {
                    await context.Translate.AddRangeAsync(GetTranslates());
                    saveChanges = true;
                }

                if (saveChanges)
                {
                    await context.SaveChangesAsync(System.Threading.CancellationToken.None);
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<MavDbContext>();
                logger.LogError(ex.Message);
            }
        }

        private static IReadOnlyList<Language> GetLanguages()
        {
            var json = File.ReadAllText("../MAV.Cms.Infrastructure/Data/SeedData/Language.json");
            var data = JsonSerializer.Deserialize<IReadOnlyList<Language>>(json);
            for (int i = 0; i < data.Count; i++)
            {
                data.ElementAt(i).CreatedById = Guid.Parse("A24F2FE2-B8E0-421E-B263-39846F05F229");
            }
            return data;
        }
        private static IReadOnlyList<CustomVar> GetCustomVars()
        {
            var json = File.ReadAllText("../MAV.Cms.Infrastructure/Data/SeedData/CustomVar.json");
            var data = JsonSerializer.Deserialize<IReadOnlyList<CustomVar>>(json);
            for (int i = 0; i < data.Count; i++)
            {
                data.ElementAt(i).CreatedById = Guid.Parse("A24F2FE2-B8E0-421E-B263-39846F05F229");
            }
            return data;
        }
        private static IReadOnlyList<Translate> GetTranslates()
        {
            var json = File.ReadAllText("../MAV.Cms.Infrastructure/Data/SeedData/Translate.json");
            var data = JsonSerializer.Deserialize<IReadOnlyList<Translate>>(json);
            for (int i = 0; i < data.Count; i++)
            {
                data.ElementAt(i).CreatedById = Guid.Parse("A24F2FE2-B8E0-421E-B263-39846F05F229");
            }
            return data;
        }
        private static IReadOnlyList<Menu> GetMenus()
        {
            var json = File.ReadAllText("../MAV.Cms.Infrastructure/Data/SeedData/Menu.json");
            var data = JsonSerializer.Deserialize<IReadOnlyList<Menu>>(json);
            for (int i = 0; i < data.Count; i++)
            {
                data.ElementAt(i).CreatedById = Guid.Parse("A24F2FE2-B8E0-421E-B263-39846F05F229");
            }
            return data;
        }
        private static IReadOnlyList<Page> GetPages()
        {
            var json = File.ReadAllText("../MAV.Cms.Infrastructure/Data/SeedData/Page.json");
            var data = JsonSerializer.Deserialize<IReadOnlyList<Page>>(json);
            for (int i = 0; i < data.Count; i++)
            {
                data.ElementAt(i).CreatedById = Guid.Parse("A24F2FE2-B8E0-421E-B263-39846F05F229");
            }
            return data;
        }
        private static IReadOnlyList<Slide> GetSlides()
        {
            var json = File.ReadAllText("../MAV.Cms.Infrastructure/Data/SeedData/Slide.json");
            var data = JsonSerializer.Deserialize<IReadOnlyList<Slide>>(json);
            for (int i = 0; i < data.Count; i++)
            {
                data.ElementAt(i).CreatedById = Guid.Parse("A24F2FE2-B8E0-421E-B263-39846F05F229");
            }
            return data;
        }
        private static IReadOnlyList<GeneralSettings> GetGeneralSettings()
        {
            var json = File.ReadAllText("../MAV.Cms.Infrastructure/Data/SeedData/GeneralSettings.json");
            var data = JsonSerializer.Deserialize<IReadOnlyList<GeneralSettings>>(json);
            for (int i = 0; i < data.Count; i++)
            {
                data.ElementAt(i).CreatedById = Guid.Parse("A24F2FE2-B8E0-421E-B263-39846F05F229");
            }
            return data;
        }

        public static async Task SeedUserAsync(UserManager<MavUser> userManager, RoleManager<MavRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            if (await roleManager.Roles.AnyAsync()) return;

            var roles = new List<MavRole>
            {
                new MavRole{Name = "Author"},
                new MavRole{Name = "Admin"},
                new MavRole{Name = "SuperAdmin"},
            };

            for (int i = 0; i < roles.Count; i++)
            {
                await roleManager.CreateAsync(roles[i]);
            }

            var admin = new MavUser
            {
                Id = Guid.Parse("A24F2FE2-B8E0-421E-B263-39846F05F229"),
                Name = "Erbay",
                Surname = "Mavzer",
                UserName = "mavzerbay",
                Email = "mavzerbay@gmail.com",
                PhoneNumber = "(0553) 283 0310",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            await userManager.CreateAsync(admin, "Password123*");
            await userManager.AddToRoleAsync(admin, "SuperAdmin");

        }
    }
}
