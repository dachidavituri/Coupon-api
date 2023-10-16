using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Models.Validations;
using WebApplication1.store;
using static WebApplication1.store.CouponStore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// get coupons

app.MapGet("/api/coupon", (ILogger<Program> _logger) =>

{
    ApiResponse response = new();
    _logger.Log(LogLevel.Information, "getting all coupons");
    response.Result = CouponStore1.couponList;
    response.Success = true;
    response.httpStatusCode = System.Net.HttpStatusCode.OK;
    return Results.Ok(response);
}).WithName("getCoupons");
// get coupons by id filter

app.MapGet("/api/coupon/{id:int}", (int id, ILogger<Program> _logger) =>
{
    _logger.Log(LogLevel.Information, "getting coupon " + id);
    return Results.Ok(CouponStore1.couponList.FirstOrDefault(x => x.Id == id));
}).WithName("get Coupon");

// add coupon post method



app.MapPost("/api/coupon",  (IValidator <CouponDTO> _validation, [FromBody] CouponDTO coupon_c_DTO) =>
{
    var validationResult = _validation.ValidateAsync(coupon_c_DTO).GetAwaiter().GetResult();
    if(!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.FirstOrDefault().ToString());
    }if(CouponStore1.couponList.FirstOrDefault(x => x.Name.ToLower() == coupon_c_DTO.Name.ToLower()) != null)
    {
        return Results.BadRequest("Coupon already exist");
    }
    Coupon coupon = new()
    { 
        IsActive = coupon_c_DTO.IsActive,
        Name = coupon_c_DTO.Name,
        Percent = coupon_c_DTO.Percent
    };

    coupon.Id = CouponStore1.couponList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
    CouponStore1.couponList.Add(coupon);
    return Results.Ok(coupon);

});


//update method put
app.MapPut("/api/coupon", async (IValidator<UpdateCoupon> _validation, [FromBody] UpdateCoupon coupon_c_DTO) =>
{
    ApiResponse response = new ApiResponse { Success = true, httpStatusCode = System.Net.HttpStatusCode.OK };

    var validationResult = await _validation.ValidateAsync(coupon_c_DTO);

    if (!validationResult.IsValid)
    {
        response.Success = false;
        response.httpStatusCode = System.Net.HttpStatusCode.BadRequest;
        response.Errors1.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    Coupon CouponStore = CouponStore1.couponList.FirstOrDefault(x => x.Id == coupon_c_DTO.Id);
    CouponStore.IsActive = coupon_c_DTO.IsActive;
    CouponStore.Name = coupon_c_DTO.Name;
    CouponStore.Percent = coupon_c_DTO.Percent;
    CouponStore.LastUpdated = DateTime.Now;

    response.Success = true;
    response.httpStatusCode =System.Net.HttpStatusCode.OK;
    return Results.Ok(response);

    
});



// delete method 
app.MapDelete("/api/delete/{id:int}", (int id) =>
{
    ApiResponse response = new() { Success = false, httpStatusCode = System.Net.HttpStatusCode.BadRequest };
    Coupon Coupon = CouponStore1.couponList.FirstOrDefault(x => x.Id == id);
    if (Coupon != null)
    {
        CouponStore1.couponList.Remove(Coupon);
        response.Success = true;
        response.httpStatusCode = System.Net.HttpStatusCode.NoContent;
        return Results.Ok(response);
    }
    else
    {
        response.Errors1.Add("invalid");
        return Results.BadRequest(response);
    };

});



app.UseHttpsRedirection();



app.Run();


