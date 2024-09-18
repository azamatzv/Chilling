using Entities.Model.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = new Guid(("9245fe4a-d402-451c-b9ed-9c1a04247482")),
                FirstName = "Azamat",
                LastName = "Zokirov",
                Login = "admin",
                Password = "admin",
                Role = RoleEnum.Admin,
                isDeleted = false
            },
            new User
            {
                Id = new Guid(("9245fe4a-d402-451c-b9ed-9c1a04247483")),
                FirstName = "Abdulloh",
                LastName = "Tolibov",
                Login = "user",
                Password = "user",
                Role = RoleEnum.User,
                isDeleted = false
            });
    }
}