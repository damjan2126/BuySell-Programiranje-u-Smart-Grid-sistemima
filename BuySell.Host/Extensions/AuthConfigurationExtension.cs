﻿using BuySell.Business.Services;
using BuySell.Business.Services.Contracts;
using BuySell.Business.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BuySell.Host.Extensions
{
    public static class AuthConfigurationExtension
    {
        public static void ConfigureAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication()
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(builder.Configuration["Authorization:Secret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.Configure<AuthorizationSettings>(builder.Configuration.GetSection("Authorization"));
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin",
                      policy =>
                      {
                          policy.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
                          policy.RequireClaim("Roles", new List<string>() { "Admin" });
                      });

                opt.AddPolicy("Seller",
                      policy =>
                      {
                          policy.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
                          policy.RequireClaim("Roles", new List<string>() { "Seller" });
                      });

                opt.AddPolicy("Buyer",
                      policy =>
                      {
                          policy.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
                          policy.RequireClaim("Roles", new List<string>() { "Buyer" });
                      });

                opt.AddPolicy("Active",
                      policy =>
                      {
                          policy.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
                          policy.RequireClaim("Roles", new List<string>() { "Active" });
                      });

                opt.AddPolicy("ActiveSeller",
                    policy =>
                    {
                        policy.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
                        policy.RequireAssertion(context =>
                        {
                            var user = context.User;

                            var hasSellerClaim = user.HasClaim(c => c.Type == "Roles" && c.Value == "Seller");
                            var hasActiveClaim = user.HasClaim(c => c.Type == "Roles" && c.Value == "Active");

                            return hasSellerClaim && hasActiveClaim;
                        });
                    });
            });

            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}
