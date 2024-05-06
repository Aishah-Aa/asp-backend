using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CodeCrafters_backend_teamwork.src.Entities;

    public class OrderCheckout 
    {
        public Guid Id {get ; set;}
        public int PaymentId {get ; set;}
        public int UsersId {get ; set;}
        public int ShippingId {get ; set;}
        public string Status {get ; set;}
        public double TotalPrice {get ; set;}

        public OrderCheckout( DateTime createdAt ,int usersId , int shippingId ,int paymentId, string status ,double totalPrice)
    {
        Id = Guid.NewGuid();
        PaymentId =  paymentId;
        UsersId = usersId;
        ShippingId = shippingId;
        Status = status;
        TotalPrice = totalPrice; 
    }

    }

    