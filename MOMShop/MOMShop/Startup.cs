using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MOMShop.MomShopDbContext;
using MOMShop.MomShopMapperProfile;
using MOMShop.Services.Implements;
using MOMShop.Services.Implements.Payment;
using MOMShop.Services.Implements.UserProductService;
using MOMShop.Services.Interfaces;
using MOMShop.Services.Interfaces.Mail;
using MOMShop.Services.Interfaces.PaymentService;
using MOMShop.Services.Interfaces.UserService;
using MOMShop.Utils.Mail;
using MOMShop.Utils.Payment;

namespace MOMShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MOMShop", Version = "v1" });
            });
            services.AddOptions();                                         // Kích hoạt Options
            var mailsettings = Configuration.GetSection("MailSettings");  // đọc config
            services.Configure<MailSettings>(mailsettings);                // đăng ký để Inject

            services.AddOptions();                                         // Kích hoạt Options
            var vnPaySettings = Configuration.GetSection("VNPaySettings");  // đọc config
            services.Configure<VNPaySettings>(vnPaySettings);

            services.AddAutoMapper(typeof(MapperProfile));
            services.AddHttpContextAccessor();
            //Add Services
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IProductDetailServices, ProductDetailServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IOrderDetailServices, OrderDetailServices>();
            services.AddScoped<IReceiveOrderServices, ReceiveOrderServices>();
            services.AddScoped<IReceiveOrderDetailService, ReceiveOrderDetailService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<IUserProductService, UserProductService>();
            services.AddScoped<IUserCartService, UserCartService>();
            services.AddScoped<IUserOrderService, UserOrderService>();
            services.AddScoped<IUserCollectionService, UserCollectionService>();
            services.AddScoped<IDiscountService, DiscoutService>();
            services.AddScoped<IUserFeedbackService, UserFeedBackService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddTransient<ISendMailService, SendMailService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICustomerServices, CustomerService>();
            services.AddScoped<IFeedbackServices, FeedbackService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MOMShop v1"));
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
