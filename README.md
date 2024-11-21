# Budget Pulse Web Application

## Overview
**Budget Pulse** is a financial management web application designed to help users track expenses, manage budgets, and forecast spending. Built with **MVC architecture** and developed using **C# and .NET**, the application provides a seamless and intuitive user experience for financial planning. The backend is powered by a robust **AWS-hosted PostgreSQL database**, ensuring secure and scalable data storage.

---

## Key Features
- **Expense Tracking**: Log and categorize daily expenses for detailed insights.
- **Budget Management**: Create, edit, and monitor budgets across multiple categories.
- **Spending Forecasting**: Leverage past trends to predict future expenses.
- **Secure Authentication**: User accounts are secured with modern authentication mechanisms.
- **Cloud Deployment**: Hosted on AWS for high availability and performance.
- **Interactive UI**: Intuitive web interface following MVC principles for clean separation of concerns.

---

## Technical Stack
- **Frontend**:
  - Razor Pages for dynamic HTML rendering.
  - Bootstrap for responsive UI design.
- **Backend**:
  - **C# .NET Framework** for server-side logic.
  - **Entity Framework** for object-relational mapping (ORM).
- **Database**:
  - PostgreSQL hosted on **AWS RDS** for scalable and reliable storage.
- **Deployment**:
  - Hosted on **AWS EC2** with **AWS Elastic Beanstalk** for auto-scaling.
  - Continuous Integration/Deployment (CI/CD) using **GitHub Actions**.

---

## Architecture
The application follows the **Model-View-Controller (MVC)** design pattern:
1. **Model**: Represents the core business logic and database interactions.
2. **View**: Handles the presentation layer, ensuring a clean and user-friendly interface.
3. **Controller**: Orchestrates the flow of data between the Model and the View.

---

## Workflow
1. **User Actions**:
   - Users log in to their account, view current budgets, or add new expenses.
2. **Controller Logic**:
   - Requests are processed by controllers to validate inputs and interact with models.
3. **Database Interaction**:
   - Models use Entity Framework to query the AWS-hosted PostgreSQL database.
4. **View Rendering**:
   - The Razor View Engine dynamically renders web pages with the processed data.

---

## Deployment Details
- **Infrastructure**:
  - Deployed on **AWS EC2 instances** for hosting.
  - **AWS RDS** powers the PostgreSQL database.
- **Security**:
  - HTTPS with SSL encryption for secure data transmission.
  - AWS IAM for access control.
- **Scalability**:
  - Load balancing via **AWS Elastic Load Balancer (ELB)**.
  - Automatic scaling through **AWS Auto Scaling Groups**.
