using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OganiApp.Core.Entities;
using OganiApp.Service.FluentValidations;
using OganiApp.Service.Models.Account;


namespace OganiApp.Service.Extensions
{
    public static class ValidationExtension
    {
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Product>, ProductValidation>();
            services.AddScoped<IValidator<ProductDetail>, ProductDetailValidation>();

            services.AddScoped<IValidator<Blog>, BlogValidation>();
            services.AddScoped<IValidator<BlogDetail>, BlogDetailValidator>();

            services.AddScoped<IValidator<Owner>, OwnerValidation>();
            services.AddScoped<IValidator<Category>, CategoryValidation>();
            services.AddScoped<IValidator<Comment>, CommentValidation>();
            services.AddScoped<IValidator<Advert>, AdvertValidation>();
            services.AddScoped<IValidator<Contact>, ContactValidation>();

            services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
        }
    }
}
