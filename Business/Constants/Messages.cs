using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants {
    public static class Messages {
        public static string ItemAdded = "--Item added: ";
        public static string ItemUpdated = "--Item updated: ";
        public static string ItemDeleted = "--Item deleted: ";
        public static string ItemListed = "--Item Listed";
        public static string ItemsListed = "--Items Listed";
        public static string DetailedItemsListed = "--Detailed items Listed";
        public static string ItemNameInvalid = "!!!Min name length is 2";
        public static string ItemDailyPriceInvalid = "!!!Daily price must he higher than 0";
        public static string UserNotExist = "!!!User doesn't exist";
        public static string CarAlreadyRented = "!!!Car is already rented";
        public static string CarImageExceeded = "Car image count exceeded";
        public static string AuthorizationDenied = "Authorization denied";
        public static string AccessTokenCreated = "Access token created";
        public static string UserAlreadyExists = "User already exists";
        public static string SuccessfulLogin = "Successful login";
        public static string PasswordError = "Password error";
        public static string UserNotFound = "User not found";
        public static string UserRegistered = "User registered";
    }
}
