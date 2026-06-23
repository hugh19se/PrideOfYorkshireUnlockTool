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

                APIHandler apiHandler = new();

                Console.WriteLine("\nAttempting Login...");
                KeyValuePair<int, string> userInfo = await apiHandler.AttemptLogin(emailAddress, password);
                Console.WriteLine($"Successfully Logged In\nUser ID: {userInfo.Key}\nDisplay Name: {userInfo.Value}");

                Console.WriteLine("\nRetrieving List Of Sculptures...");
                IEnumerable<string> sculptures = await apiHandler.GetSculptureIDs();

                Console.WriteLine("\nUnlocking All Available Sculptures...");
                IEnumerable<string> acceptedSculptureIDs = await apiHandler.UnlockSculptures(sculptures);
                Console.WriteLine($"Unlocked {acceptedSculptureIDs.Count()} Sculptures");

                IEnumerable<string> notAcceptedSculptures = sculptures.Where(x => !acceptedSculptureIDs.Contains(x));
                if(notAcceptedSculptures.Any())
                {
                    Console.WriteLine("\nThe Following Sculpture IDs Were NOT Accepted");
                    foreach (string sculptureId in notAcceptedSculptures)
                    {
                        Console.WriteLine(sculptureId);
                    }
                }
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