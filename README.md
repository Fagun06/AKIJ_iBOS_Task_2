
# **Order and Product Management API**

This project is an ASP.NET Core Web API designed for managing products, orders, and customers. It provides robust CRUD operations for products and orders, as well as advanced features like transaction handling and data reporting.

---

## **Features**
1. **Product Management**
   - Add new products.
   - Retrieve low-stock products below a specified quantity.
   - View unordered products.

2. **Order Management**
   - Create orders with stock validation.
   - Update order quantities and adjust stock.
   - Delete orders while restoring product stock.

3. **Reports**
   - Retrieve detailed order information with product details.
   - Summarize product orders (total quantity and revenue).
   - Get the top 3 customers based on total quantity ordered.

4. **Bulk Operations**
   - Support for bulk order creation with transactional rollback for failed orders.

---

## **Task Details**

### **Database Schema**
- **Table 1: tblProducts**
  - `intProductId` (Identity Column, integer): Primary key for each product.
  - `strProductName` (nvarchar): Name of the product.
  - `numUnitPrice` (decimal): Unit price of the product.
  - `numStock` (decimal): Current stock quantity of the product.

- **Table 2: tblOrders**
  - `intOrderId` (Identity Column, integer): Primary key for each order.
  - `intProductId` (integer, Foreign Key): References `tblProducts.intProductId`.
  - `strCustomerName` (nvarchar): Name of the customer who placed the order.
  - `numQuantity` (decimal): Quantity ordered.
  - `dtOrderDate` (datetime): Date of the order.

### **Task Requirements**
1. **API 01:** Create a new order for an existing product.
   - Place an order for a product (e.g., "Tool C" by customer "Alex Brown" with a quantity of 25).
   - Check stock availability before creating the order.
   - Return a message indicating insufficient stock if applicable.
   - Deduct the ordered quantity from `tblProducts.numStock` after creating the order.

2. **API 02:** Update an order's quantity.
   - Update an order (e.g., order ID 2) to increase the quantity.
   - Ensure the updated quantity does not exceed the available stock.
   - Adjust the quantity in `tblOrders` and update `tblProducts.numStock` accordingly.

3. **API 03:** Delete an order by `intOrderId`.
   - Restore the quantity to `tblProducts.numStock` upon deletion.

4. **API 04:** Retrieve all orders with product details.
   - Return each order along with `strProductName` and `numUnitPrice`.

5. **API 05:** Get a summary of total quantity ordered and total revenue for each product.
   - Include:
     - Product name.
     - Total quantity ordered.
     - Total revenue (calculated as `numQuantity * numUnitPrice`).

6. **API 06:** Retrieve all products with a stock quantity below a specified threshold.
   - Return product names, unit prices, and stock quantities.

7. **API 07:** Get the top 3 customers by total quantity ordered.

8. **API 08:** Find products that have not been ordered at all.
   - Identify records in `tblProducts` with no corresponding entries in `tblOrders`.

9. **API 09:** Implement a transactional operation for bulk order creation.
   - Accept a list of orders.
   - Validate each order (for stock availability).
   - Insert them as a single transaction.
   - Roll back the entire operation if any order fails.

---

## **Technologies Used**
- **Backend Framework:** ASP.NET Core
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Language:** C#

---

## **Setup Instructions**

### **Prerequisites**
- Install **Visual Studio 2022** or later.
- Install **.NET SDK 6.0** or later.
- Install **SQL Server** and SQL Server Management Studio (SSMS).

---

## **Contact**
For issues or contributions, feel free to contact:

- **Author:** Your Name
- **Email:** your.email@example.com
- **GitHub:** [Fagun06](https://github.com/Fagun06)
