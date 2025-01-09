namespace pfm;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

	      var capacity = await GetCapacity();

        if (count == 1)
            CounterBtn.Text = $"Picó {count} vez la pata gorda. Capacidad: {capacity}%";
        else
            CounterBtn.Text = $"Picó {count} veces la pata gorda. Capacidad: {capacity}%";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private static async Task<int> GetCapacity()
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Api_key", "6B9A27BB-CD5C-4004-8ACB-6323E8A619A9");

        try
        {
            var response = await httpClient.GetAsync("https://v2.twinoaksadvantage.com/tosdapi/api/MemberInformation/GetCrowdLevel?clubId=3098");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var quota = (int)(decimal.Parse(responseBody) * 100);
                return quota;
            }

            Console.WriteLine("Error en la llamada: " + response.StatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en la llamada: " + ex.Message);
        }

        Console.WriteLine("No capacity info");
        return 0;
    }
}

