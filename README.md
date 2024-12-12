# API-Template-DDD-NET
Una plantilla para APIs .net Arquitectura limpia DDD

# Documentación del Proyecto: Domain-Driven Design en .NET Core

## Introducción

Este documento describe un proyecto desarrollado para usarse de plantilla siguiendo los principios de Domain-Driven Design (DDD) implementado en .NET Core. El objetivo principal es estructurar la aplicación de manera que el dominio del negocio sea el eje central del desarrollo, asegurando flexibilidad, mantenibilidad y claridad.

## Arquitectura del Proyecto

El proyecto está dividido en las siguientes capas:

1. **Capa de Dominio (Domain):**
   - Contiene las entidades, objetos de valor, agregados, repositorios y servicios de dominio.
   - Representa las reglas de negocio y la lógica central de la aplicación.

2. **Capa de Aplicación (Application):**
   - Incluye los casos de uso y la coordinación de las operaciones del dominio.
   - Define interfaces para servicios y repositorios, utilizados por la capa de infraestructura.

3. **Capa de Infraestructura (Infrastructure):**
   - Implementa la persistencia, integraciones externas y otros detalles técnicos.
   - Contiene la configuración de Entity Framework Core, servicios externos y otros adaptadores.

4. **Capa de Presentación (Presentation):**
   - Implementa las API RESTful o interfaces de usuario.
   - Maneja la comunicación entre los usuarios y el sistema.

## Detalles de Implementación

### 1. Dominio

- **Entidades:** Clases que representan los conceptos principales del negocio y tienen identidad.
  ```csharp
  public class Order
  {
      public Guid Id { get; private set; }
      public Customer Customer { get; private set; }
      public List<OrderItem> Items { get; private set; }
      public decimal TotalAmount => Items.Sum(item => item.Price * item.Quantity);

      public void AddItem(OrderItem item) { /* ... */ }
  }
  ```

- **Objetos de Valor:** Clases inmutables que representan conceptos sin identidad propia.
  ```csharp
  public class Address
  {
      public string Street { get; }
      public string City { get; }
      public string PostalCode { get; }
  }
  ```

- **Servicios de Dominio:** Contienen lógica de negocio que no pertenece a una entidad u objeto de valor específico.

### 2. Aplicación

Orquestación: Coordina las interacciones entre la capa de dominio, infraestructura, y la capa de presentación.
Aislamiento: Evita que la lógica de negocio del dominio se mezcle con la infraestructura o la presentación.
Transparencia: Proporciona una API consistente que puede ser usada por controladores, servicios externos o pruebas automatizadas.

.NET Core cumple con la función de coordinar las operaciones de negocio y servir de intermediario entre la capa de dominio y las interfaces externas (como API o interfaces de usuario).

1. DTOs (Data Transfer Objects):
   Contienen modelos simples usados para transferir datos entre la capa de aplicación y la capa de presentación (o entre la aplicación y servicios externos).
   Ejemplo: CreateClientDTO.cs
        
2. Interfaces:
   Definen contratos para los servicios que implementan lógica de negocio en esta capa.
   Ejemplo: IClientService.cs probablemente describe las operaciones que pueden realizarse relacionadas con los clientes (crear, obtener, actualizar, eliminar).

3. Mapping (Automapper):
   Contiene configuraciones para mapear objetos entre diferentes capas (como DTOs y entidades del dominio) usando AutoMapper.
   Ejemplo: MappingProfile.cs

4. Services:
   - Contiene la implementación de los casos de uso o lógica de la aplicación.
   - Por ejemplo, ClientAppService o ClientService implementan las operaciones definidas en IClientService. Estos servicios interactúan con la capa de dominio y otros servicios, como            repositorios.

   
- **Casos de Uso:** Orquestan la lógica del dominio para cumplir con los requisitos del negocio.
  ```csharp
  public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
  {
      private readonly IOrderRepository _orderRepository;

      public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
      {
          var order = new Order(/* ... */);
          await _orderRepository.AddAsync(order);
          return order.Id;
      }
  }
  ```

### 3. Infraestructura

- **Persistencia:** Implementada con Entity Framework Core o cualquier otro ORM.
  ```csharp
  public class OrderRepository : IOrderRepository
  {
      private readonly AppDbContext _context;

      public async Task AddAsync(Order order) { /* ... */ }
  }
  ```

- **Configuración de Dependencias:** Se utiliza inyección de dependencias a través de .NET Core.
  ```csharp
  services.AddScoped<IOrderRepository, OrderRepository>();
  ```

### 4. Presentación

- **API REST:** Implementada con ASP.NET Core Web API.
  ```csharp
  [ApiController]
  [Route("api/[controller]")]
  public class OrdersController : ControllerBase
  {
      [HttpPost]
      public async Task<IActionResult> Create(CreateOrderCommand command) { /* ... */ }
  }
  ```

## Patrones y Principios Utilizados

1. **Agregados y Raíces de Agregado:** Para garantizar consistencia en las operaciones del dominio.
2. **Inyección de Dependencias:** Facilita el desacoplamiento entre capas.
3. **CQRS (Command and Query Responsibility Segregation):** Para separar lecturas de escrituras.
4. **Repositorios:** Para abstraer el acceso a datos.

## Tecnologías y Herramientas

- **.NET Core:** Framework principal.
- **Entity Framework Core:** ORM para acceso a datos.
- **MediatR:** Para implementar patrones CQRS.
- **Swagger:** Documentación de la API.
- **Automapper:** Para transformar objetos entre capas.

## Próximos Pasos

1. Implementar pruebas unitarias para las capas de dominio y aplicación.
2. Ampliar la documentación con ejemplos de configuración.
3. Optimizar el rendimiento de las consultas utilizando proyecciones con LINQ.

## Conclusión

Este proyecto proporciona una base sólida para el desarrollo de aplicaciones robustas y escalables mediante el uso de Domain-Driven Design. La estructura modular facilita el mantenimiento y la extensión del sistema a medida que evolucionan los requisitos del negocio.


