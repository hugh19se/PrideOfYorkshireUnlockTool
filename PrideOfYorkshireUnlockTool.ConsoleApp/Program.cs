namespace PrideOfYorkshireUnlockTool.ConsoleApp
{
    internal class Program
    {
        static async Task Main()
        {
            try
            {
                Console.WriteLine("Enter User Email");
                string emailAddress = Console.ReadLine() ?? throw new FormatException("Invalid Email Address Entered");

                Console.WriteLine("Enter User Password");
                string password = Console.ReadLine() ?? throw new FormatException("Invalid Password Entered");

                Console.WriteLine("\nAttempting Login...");
                APIHandler apiHandler = new();
                KeyValuePair<int, string> userInfo = await apiHandler.AttemptLogin(emailAddress, password);
                Console.WriteLine($"Successfully Logged In\nUser ID: {userInfo.Key}\nDisplay Name: {userInfo.Value}\n");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Unexpected Error Has Occurred\n{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine("Exiting Program...");
            }
        }
    }
}