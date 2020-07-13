namespace Passenger.Infrastructure.Exceptions
{
    public static class ErrorCodes
    {
        public static string EmailInUse => "email_in_use";
        public static string InvalidCredentials => "invalid_credentials";
        public static string InvalidUsername => "invalid_username";
        public static string DriverNotFound => "driver_not_found";
        public static string DriverAlreadyExist => "driver_already_exist";
        public static string UserNotFound => "user_not_found";
    }
}
