 POS System (Console Application)

 Description

A Console-based Point of Sale (POS) System developed in C#. The application allows users to manage products, add items to a shopping cart, complete customer checkout, generate receipts, and view order history.

---

 Features

- Product Management
- Shopping Cart Management
- Checkout System
- Receipt Generation
- Order History

---

 Concepts Used

- Object-Oriented Programming (OOP)
- Classes & Objects
- Encapsulation
- Inheritance
- Polymorphism
- Interfaces
- Exception Handling
- List<T>
- Dictionary<TKey, TValue>
- Queue<T>
- Stack<T>
- Generics
- IReadOnlyCollection
- StringBuilder
- JSON Serialization (Newtonsoft.Json)

---

 Project Structure

```
PosSystem
│
├── Data
│   └── Products.csv
│
├── Models
│   ├── Product.cs
│   ├── Cart.cs
│   ├── CartItem.cs
│   └── Order.cs
│
├── Services
│   ├── ProductService.cs
│   ├── CartService.cs
│   ├── CheckoutService.cs
│   └── ProductLoader.cs
│
├── UI
│   └── MenuUI.cs
│
├── Program.cs
│
└── README.md
---

 NuGet Package

- Newtonsoft.Json

Used to serialize completed orders into JSON format.

---
 Author

-Faseeh Ur Rehman-