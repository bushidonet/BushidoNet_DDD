// // public partial class Perfil
// // {
// //     // Propiedades para almacenar los datos del perfil
// //     public string Nombre { get; set; } = "Juan Pérez";
// //     public string Email { get; set; } = "juan.perez@example.com";
// //     public string Descripcion { get; set; } = "Desarrollador web con más de 5 años de experiencia.";
// // }
//
// private int currentCount = 0;
//
//     private bool _disposed = false;
//     private List<Client> clients = new();
//     private List<Client> filteredClients = new();
//     private Client selectedClient = null;
//     private Client newClient= new() { BirthDate = DateTime.Now };
//     private CreateClientDTO addNewClient = new() { BirthDate = DateTime.Now };
//     private string searchTerm = string.Empty;
//     
//     private bool showHistory = false;
//     private List<string> clientHistory = new();
//     
//     protected override async Task OnInitializedAsync()
//     {
//         if (!_disposed) // Verificar antes de actualizar el estado
//         {
//             await FetchClients();
//         }
//     }   
//     private void ShowClientHistory()
//     {
//         if (selectedClient == null) return;
//         
//         showHistory = true;
//
//         // Simulación de carga de historial (puedes hacer una llamada a la API aquí)
//         clientHistory = new List<string>
//         {
//             $"Compra realizada el {DateTime.Now.AddDays(-10):dd/MM/yyyy}",
//             $"Actualización de datos el {DateTime.Now.AddDays(-5):dd/MM/yyyy}",
//             $"Contacto con soporte el {DateTime.Now.AddDays(-2):dd/MM/yyyy}"
//         };
//         if (!_disposed) // Verificar antes de actualizar el estado
//         {
//             StateHasChanged();        
//         }
//     }
//
//     private void CloseModal()
//     {
//         showHistory = false;
//     }
//     
//     private async Task FetchClients()
//     {
//         try
//         {
//             var response = await Http.GetFromJsonAsync<List<Client>>("http://localhost:8080/api/Client/getAllClient");
//             clients = response ?? new List<Client>();
//             filteredClients = clients;
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Error al obtener clientes: {ex.Message}");
//         }
//     }
//     
//     private void HandleSearchChange(ChangeEventArgs e)
//     {
//         searchTerm = e.Value.ToString();
//         filteredClients = clients.Where(c =>
//             c.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
//             c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
//             c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
//         ).ToList();
//     }
//     
//     private void HandleSelectClient(Client client)
//     {
//         selectedClient = client;
//         addNewClient = new CreateClientDTO()
//         {
//             
//             FirstName = client.FirstName,
//             LastName = client.LastName,
//             Email = client.Email,
//             Phone = client.Phone,
//             BirthDate = client.BirthDate
//         };
//     }
//     
//     private async Task HandleSubmit()
//     {
//         try
//         {
//             if (selectedClient != null)
//             {
//                 // Actualizar cliente existente usando Client
//                 var updatedClient = new Client
//                 {
//                     Id = selectedClient.Id,
//                     FirstName = addNewClient.FirstName,
//                     LastName = addNewClient.LastName,
//                     Email = addNewClient.Email,
//                     Phone = addNewClient.Phone,
//                     BirthDate = addNewClient.BirthDate
//                 };
//                 // Actualizar cliente
//                 var response = await Http.PutAsJsonAsync($"http://localhost:8080/api/Client/updateClient/{selectedClient.Id}", updatedClient);
//                 if (response.IsSuccessStatusCode)
//                 {
//                     Console.WriteLine("Cliente registrado correctamente.");
//                     await FetchClients(); // Recargar lista de clientes
//                     HandleClear(); // Limpiar formulario
//                 }
//                 else
//                 {
//                     string errorMessage = await response.Content.ReadAsStringAsync();
//                     Console.WriteLine($"Error en la API: {response.StatusCode} - {errorMessage}");
//                 }
//             }
//             else
//             {
//                 // Registrar nuevo cliente
//                 
//                 var response = await Http.PostAsJsonAsync("http://localhost:8080/api/Client/addClient", addNewClient);
//                 if (response.IsSuccessStatusCode)
//                 {
//                     Console.WriteLine("Cliente registrado correctamente.");
//                     await FetchClients(); // Recargar lista de clientes
//                     HandleClear(); // Limpiar formulario
//                 }
//                 else
//                 {
//                     string errorMessage = await response.Content.ReadAsStringAsync();
//                     Console.WriteLine($"Error en la API: {response.StatusCode} - {errorMessage}");
//                 }
//             }
//     
//            
//             await FetchClients();
//             HandleClear();
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Error al guardar cliente: {ex.Message}");
//         }
//     }
//
//     private void HandleClear()
//     {
//         // Si el modal está abierto, no limpiamos selectedClient para que el historial siga mostrando
//         if (!showHistory)
//         {
//             addNewClient = new CreateClientDTO() { BirthDate = DateTime.Now };
//             selectedClient = null;
//         }
//         else
//         {
//             addNewClient = new CreateClientDTO() { BirthDate = DateTime.Now };
//         }
//     }
//
//     private async Task HandleDelete()
//     {
//         if (selectedClient != null)
//         {
//             bool confirmDelete = await JSRuntime.InvokeAsync<bool>("confirm", 
//                 $"¿Estás seguro de que deseas eliminar a {selectedClient.FirstName} {selectedClient.LastName}?");
//             
//             if (confirmDelete)
//             {
//                   var response = await Http.DeleteAsync($"http://localhost:8080/api/Client/deleteClient/{selectedClient.Id}");
//             
//
//             //Eliminar cliente
//             if (response.IsSuccessStatusCode)
//             {
//                 Console.WriteLine("Cliente registrado correctamente.");
//                 
//             }
//             else
//             {
//                 string errorMessage = await response.Content.ReadAsStringAsync();
//                 Console.WriteLine($"Error en la API: {response.StatusCode} - {errorMessage}");
//             }
//             
//             await FetchClients();
//             HandleClear();
//             }
//         }
//         
//         
//     }
//     public void Dispose()
//     {
//         _disposed = true;
//     }