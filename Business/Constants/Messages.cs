using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants {
    public static class Messages {
        public static readonly string ItemAdded = "--Item added: ";
        public static readonly string ItemUpdated = "--Item updated: ";
        public static readonly string ItemDeleted = "--Item deleted: ";
        public static readonly string ItemListed = "--Item Listed";
        public static readonly string ItemsListed = "--Items Listed";
        public static readonly string DetailedItemsListed = "--Detailed items Listed";
        
        public static readonly string BrandAlreadyExists = "Brand already exists";
        
        public static readonly string ColorAlreadyExists = "Color already exists";

        public static readonly string UserNotExists = "!!!User doesn't exist";
        public static readonly string UserAlreadyExists = "User already exists";
        public static readonly string PasswordError = "Password error";
        public static readonly string SuccessfulLogin = "Successful login";
        public static readonly string UserRegistered = "User registered";

        public static readonly string AuthorizationDenied = "Authorization denied";
        public static readonly string AccessTokenCreated = "Access token created";
        
        public static readonly string CarAlreadyRented = "Car is already rented in given interval";
        public static readonly string OrderConfirmed = "Order is confirmed";

        public static readonly string CarImageCountExceeded = "Car image count exceeded";
        public static readonly string ImageNotFound = "Image Not Found";


        public static readonly string InsufficientBalance = "Balance is insufficient ";
    }

}
