using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants {
    public static class Messages {

        //general messages
        public static string Added { get; } = " added successfully.";
        public static string Deleted { get; } = " deleted successfully.";
        public static string Updated { get; } = " updated successfully.";


        //auth manager
        public static string UserRegistered { get; } = "Registered successfully.";
        public static string WrongPasswordOrEmail { get; } = "Wrong password or email.";
        public static string SuccessfulLogin { get; } = "Successfull login.";
        public static string AccessTokenCreated { get; } = "Access token created.";
        public static string UserAlreadyExists { get; } = "User already exists.";


        //brand manager
        public static string BrandAlreadyExists { get; } = "Brand already exists.";
        public static string BrandAdded { get; } = "Brand" + Added;
        public static string BrandDeleted { get; } = "Brand" + Deleted;
        public static string BrandUpdated { get; } = "Brand" + Updated;


        //car image manager
        public static string ImageAdded { get; } = "Image" + Added;
        public static string ImageDeleted { get; } = "Image" + Deleted;
        public static string ImagesAdded { get; } = "Images" + Added;
        public static string ImageNotFound { get; } = "Image Not Found.";
        public static string CarImageCountExceeded { get; } = "Car image count exceeded.";


        //car manager
        public static string CarAdded { get; } = "Car" + Added;
        public static string CarDeleted { get; } = "Car" + Deleted;
        public static string CarUpdated { get; } = "Car" + Updated;


        //color manager
        public static string ColorAlreadyExists { get; } = "Color already exists.";
        public static string ColorAdded { get; } = "Color" + Added;
        public static string ColorDeleted { get; } = "Color" + Deleted;
        public static string ColorUpdated { get; } = "Color" + Updated;


        //customer manager
        public static string CustomerAdded { get; } = "Customer" + Added;
        public static string CustomerDeleted { get; } = "Customer" + Deleted;
        public static string CustomerUpdated { get; } = "Customer" + Updated;
        public static string InformationUpdated { get; } = "Information updated";
        public static string UserDoesntExists { get; } = "User doesn't exist";


        //payment manager
        public static string BalanceInsufficent { get; } = "Balance is insufficient ";


        //rental manager
        public static string CarRented { get; } = "Car rented successfully";
        public static string ReturnDateLessThanRentDate { get; } = "Return date must be later than rent date";
        public static string CarAlreadyRented { get; } = "Car is already rented in given interval";
        public static string CustomerHasNoNationalIdentity { get; } = "You need to add your national identity from your profile to rent a car";
        public static string FindexScoreInsufficient { get; } = "Findex score is insufficient ";


        //user manager
        public static string PasswordError { get; } = "Password error";
        public static string PasswordUpdated { get; } = "Password" + Updated;





        public static string AuthorizationDenied { get; } = "Authorization denied";




    }

}
