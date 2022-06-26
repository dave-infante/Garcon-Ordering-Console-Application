# Garçon - Menu Ordering Console Application
---

### Getting Started
- **Latest releases:**
	- `1.0.0`  - Initial version of the project containing stable functionalities aligning to the following project requirements:
        > The ordering application will display menu items according to their classification _(e.g., appetizer, main course, dessert, drinks)_. There is also a particular classification called _Chef's Recommended Menu_ that contains menu items considered a today's menu. Users may select menu items into a cart which can then be customized _(e.g., quantity update, removing items, clearing items)_. 

		> The ordered items are prepared and cooked based on the defined duration set and will be served only when the wait time has been completed. Items in the cart can then be _placed_ to order for processing. The user also has the option to _cancel_ the order only if there are no orders currently in process and no orders have been served. Users may still place menu items to order even though the ordered items placed are in-process. 
		
		> Once all orders have been served, the user can request an order _bill-out_ which will display the order summary, including the ordered items and the cost details _(e.g., subtotal cost, tax cost, total cost, service charge)_. After this, the application will reset and will prepare new order for the succeeding customer.

### Build and Test
- Open the _Garcon.sln_ file to view its source code. Use visual studio to build the application by going to _Build_ > _Build Solution_ or `ctrl` + `shift` + `B`. 

- The data source (in JSON file) are now saved in the _\bin\Debug\netcoreapp3.1\DataSource_ folder. 

### Code Design
- This project is a software deliverable to showcase the developer's object-oriented programming skills based on their learning from the _Applying OOP_ training course. The developers also incorporated various design patterns and coding techniques to deliver an application promoting quality and coding standards.
- Since the project was built in a console project, the developers devised a simple code architected to abstract the user interface, business layer, and data layer. This allows flexibility and scalability whenever future improvements (e.g., web page plugin, database plugin) are incorporated into the project. The developers took inspiration from emerging front-end frameworks to have a simple yet robust manipulation of console pages. This includes having a page router, console components, and controllers.
- The only 3rd party dependency used in the project is the _Newtonsoft JSON_ to allow high-performance encoding and decoding of JSON files. This lets the app interact with the said files, which happens to be its data source for menu items and ingredient supplies.

### Improvements
- This application is an initiative to showcase that a simple console application can make an improvement to a small business primarily in the food industry. There are things that may improve this app such as improving the user interface for a real-time update during order processing. Other transactions including payment processing, table booking and staff management may also be incorporated into the project to further improve the overall capability of this application.

### Getting Started
- Developers: `Dave Infante & Ed Ortiz`
- Date Developed: `March 18, 2021`
- Project Proponent: `Applying OOP Batch 4 - Magenic Manila Inc.`