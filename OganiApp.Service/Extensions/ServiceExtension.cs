using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OganiApp.Core.Entities;
using OganiApp.Service.FluentValidations;
using OganiApp.Service.Services;
using OganiApp.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductDetailService, ProductDetailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBlogDetailService, BlogDetailService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IAdvertService, AdvertService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IMessageSend, MessageSend>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IFavoriteSerivce, FavoriteService>();
            services.AddScoped<IContactService, ContactService>();
        }
    }
}
