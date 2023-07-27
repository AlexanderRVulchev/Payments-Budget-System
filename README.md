# Payments-Budget-System
## Overview
"Payments and Budget System" is a web application with educational and demonstrative purposes. It mimics (in a much smaller scale) systems used by the institutions in the government sector to make payments and manage their annual budget. You can make different bank payments, cash payments, manage beneficiaries, hire and lay off employees, pay salaries with social security and tax deductions, acquire and manage assets, create and save financial reports and redistribute budget funds between institutions.

## Technologies used
<ul>
  <li>.NET Core 6.0</li>
  <li>ASP.NET Core</li>
  <li>Entity Framework Core</li>
  <li>HTML, CSS, Bootstrap</li>
  <li>MS SQL Server</li>
  <li>NUnit</li>
  <li>Moq</li>
  <li>EP Plus (a .NET library for managing Excel files)</li>
  <li>JS</li>
</ul>

## Users
There are 4 types of users with different access to the application's functionality:

<ul>
  <li>
    <b>Guest</b> - logged off user. Guests can only view reports, which are made public by the Primary and Secondary users.
  </li>
  <li>
    <b>Secondary user</b> ("Второстепенен разпоредител с бюджет") - Represents an institution which doesn't have its own budget, but relies on the consolidated budget of a primary institution instead. Can access the core functionality of the application. Each Secondary user must be attached to a Primary user.
  </li>
  <li>
    <b>Primary user</b> ("Първостепенен разпоредител с бюджет") - Represents an institution which has its own budget funds and oversees its secondary institutions' spending. Primary users can have zero or more Secondary users related to them.
  </li>
  <li>
    <b>Administrator</b> ("Администратор") - The administrator can alter the global settings of the application and delete reports.
  </li>
</ul>

## Test credentials
You can test the application with pre-seeded data using the following credentials:

#### Secondary user
Username: daa</br>
Password: 1111

#### Primary user
Username: mc</br>
Password: 1111

#### Administrator
Username: admin</br>
Password: 1111

## Database diagram

![Diagram](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/6665988c-124b-4367-b707-608e1efece76)

# Functionality

## Table of contents
<ul>
  <li>
    <a href="#user-registration">User registration</a>
  </li>
  <li>
    <a href="#home-page">Home page</a>
  </li>
  <li>
    <a href="#beneficiaries">Beneficiaries</a>
  </li>
  <li>
    <a href="#managing-assets">Managing assets</a>
  </li>
  <li>
    <a href="#employees">Employees</a>
  </li>
    <li>
    <a href="#bank-payment---choosing-type">Bank payment - Choosing type</a>
  </li>
    <li>
    <a href="#bank-payment---support-expenses">Bank payment - Support expenses</a>
  </li>
    <li>
    <a href="#bank-payment---acquiring-assets">Bank payment - Acquiring assets</a>
  </li>
    <li>
    <a href="#bank-payment---monthly-salaries">Bank payment - Monthly salaries</a>
  </li>
  <li>
    <a href="#cash-payment">Cash payment
  </li>
    <li>
    <a href="#payment-information-page">Payment information page</a>
  </li>
   <li>
    <a href="#budget">Budget</a>
  </li>
  <li>
    <a href="#reports">Reports</a>
  </li>
  <li>
    <a href="#administration-pages">Administration pages</a>
  </li>
  <li>
    <a href="#error-page">Error page</a>
  </li>
</ul>


## User registration
On the registration page, you can create a new user profile as either Primary or Secondary user. If you want to create a Secondary user profile, you must select the Primary user you want to be connected to.

![Register](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/1ec450c7-e779-4b97-b96e-7f026de5fe1e)
</br>
<a href="#table-of-contents">Back to index table</a>
## Home page
On the home page, Guests can view the reports of the institutions, which are saved beforehand by the Primary and Secondary users.

![GuestView](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/b322c676-ad7d-41af-96fb-a60690a8192b)
</br>
</br>
Upon choosing a report, Guests receive its contents as an Excel table (.xlsx file)
</br>
</br>
![ReportExcel](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/40060a69-bc2a-492e-b431-96ddf8426bfc)
</br>
</br>
Upon login, Primary and Secondary users' home page displays short information about the user's access level and unlocks the left navigation menu for the core functionality of the application.
</br>
</br>
![LoggedInHome](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/c3805687-f5e1-46c0-9455-5d0eb9d29766)
</br>
</br>
<a href="#table-of-contents">Back to index table</a>
## Beneficiaries
Beneficiaries ("Контрагенти") are various business entities, which the institution can make bank payments towards.
### Beneficiaries information page
On the information page, you can view all your beneficiaries. You can search a beneficiary by its name, identifier, bank account and address. You can sort the results by ascending or descending order. Pagination is implemented. 

![BeneficiariesInfo](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/690194bc-4a7f-443a-be55-c6e44795ec2c)
### Add and edit a beneficiary
You can create new beneficiaries and edit existing ones.

![BeneficiariesNew](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/8349f2be-e62a-476e-aeba-316284474f9f)
![BeneficiariesEdit](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/acdd11ce-a740-4089-82f5-0c96b3df2609)

<a href="#table-of-contents">Back to index table</a>
## Managing assets
Users can manage their assets and purchase new ones. 
### Assets information page

The assets information page displays a list of all acquired assets and their status (their amortization, balance value etc.) for the selected year and month. You can choose any year and/or month to see the projected future or past status or your assets. You can filter and order the assets in ascending or descending order. Pagination is implemented. From this page, you can navigate to the information page of the payment, which was made for acquiring the asset, or open the Asset details page.

![AssetsInfo](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/4a3fec40-ea40-4837-a8ce-73b69148c9a5)

### Asset details page

This page presents detailed information about the asset's amortization plan and values for all months throughout the selected year. Different settings are used for calculating the amortization plan for the different types of assets (those settings can be changed by Administrator). 

![AssetDetails](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/fe96b143-84d3-4ab5-9761-a9ba74e0e389)
<a href="#table-of-contents">Back to index table</a>
## Employees
### Employees information page

On the information page, you can view all your employees with their name, personal identification number, gross salary, dates of hiring and layoff of the employee, and the sum of all payments which they have received. You can search an employee by their name and identifier. You can sort the results by ascending or descending order. Pagination is implemented. 

![EmployeesInfo](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/815081a8-6ab8-4df1-8e8c-854452635325)

### Hire new employee

You can hire a new employee by entering their data and the type of their contract. The contract type determines the social security deductions from their monthly salary. The employee's salary cannot be below the minimum wage (the minimum wage can be changed by the Administrator).

![EmployeesNew](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/ec0c75a8-8ce6-410f-b8dc-6ae156a43ceb)

### Edit/Layoff an employee

Editing an employee's information reveals the option to set a layoff date. Laid off personnel will stop receiving salary from the day their employment has been terminated.

![EmployeesEdit](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/84747d71-b3e9-4891-b158-5fa476c36c2b)
<a href="#table-of-contents">Back to index table</a>
## Bank payment - Choosing type

You can choose between three different types of bank payments. Support payments and Asset payments are made in favour of a beneficiary. Salaries are payments towards all active employees for a chosen month and year. For Asset payment, choosing the subtype of the payment will affect the type of the purchased assets and their future amortization values.

![BankPayment](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/f84cff3e-5d3e-4c7e-97e1-eb028c460bc9)
<a href="#table-of-contents">Back to index table</a>
## Bank payment - Support expenses
### Initiating the support payment
You can initiate support payment by entering all required fields.

![SupportPayment](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/e8a5b463-0c76-452f-9336-289ff97e305c)

### Payment details
Upon successful payment, a success message and information about the payment will be displayed.

![SupportPaymentDetails](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/f3a3d307-cfb2-40c4-a5e7-3e88f67ebbb1)
<a href="#table-of-contents">Back to index table</a>
## Bank payment - Acquiring assets
### Initiating the payment for asset acquisition
On the asset payment page, you can purchase up to five different kinds of assets. Upon entering quantities and single prices for the assets, a total sum will be calculated.

![AssetPayment](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/ef3b2296-f9c2-4339-967b-e34668c8d461)
### Payment details
Upon successful payment, a success message will be displayed and information about each of the purchased assets will be available for further details.

![AssetPaymentDetailsSuccess](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/a3a823ed-ceea-4deb-8b09-68a373bd9175)
<a href="#table-of-contents">Back to index table</a>
## Bank payment - Monthly salaries

After choosing year and month for the salaries, the application will accrue all social security expenses, tax deductions and net salaries for all personnel employed by the institution, who have at least one working day during the targeted month. Each employee will receive salary according to the days of the month they have been employed. All state employees' social security expenses are paid by the employer, while employees on job contracts divide those expenses between the employee and the employer (all deduction percentages and the tax rate can be changed by Administrator).

![SalariesPayment-1](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/6c6669df-62f5-43dc-adb3-672d6d94194a)
![SalariesPayment-2](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/fcbfa8b0-b6e8-43a2-98dd-20e8eafd6b27)
<a href="#table-of-contents">Back to index table</a>

## Cash payment
### Initiating the cash payment

You can make cash payments to your employees.

![CashPayment](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/44a91975-56b5-4e0c-a80a-b683f86f81e0)

### Cash payment details
Upon successful payment, a success message will be displayed along with information about the payment. Cash payments have their own seperate column in the reports.

![CashPaymentDetails](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/59392be5-08ff-41b5-be70-b7395b63f10d)
<a href="#table-of-contents">Back to index table</a>

## Payment information page

On this page, users can view all their payments. The results can be filtered by date and amount paid, and ordered in ascending or descending order. Pagination is implemented. Clicking on View ("Преглед") will display different information depending on the payment type. For salaries payment, a detailed payroll will be displayed. For payment for asset acquisition - a list of all purchased assets etc..

![Information](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/2dd148fb-c0fa-4825-99df-0e4920364183)
<a href="#table-of-contents">Back to index table</a>
## Budget

### Budget information page
The Budget defines the maximum allowed annual expenses for each institution regarding each payment group separately - support, assets and salaries. Users can't make payments that will exceed the budget limit for the respective payment group. </br>
Secondary users can only view their annual budgets, but can't change them in any way.</br>
Primary users can create new consolidated budgets.

![BudgetInfo](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/c12b55ce-63ec-4e33-b8bf-f9172ca994ac)
### Budget distribution page
On the budget distribution page, Primary users can view the total expense limit for the year, the allocated and unallocated funds, and can set the individual budgets for themselves and their Secondary users by redistributing the consolidated budget funds between each institution. 

![EditBudget](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/15ac1923-2415-4916-89dc-59a62c275161)
<a href="#table-of-contents">Back to index table</a>
## Reports
### Report Inquiry page
On the report inquiry page, users can view their saved reports and generate new ones by selecting the year and month. A report will be generated that will reflect all expenses from the start of the target year, to the last day of the selected month.</br>
Users can choose to save the report and by doing so, they make the report public so it can be viewed by Guests.
Secondary users can generate and save individual reports.</br>
Primary users can also generate and save consolidated reports, which include not only their own financial transactions, but also transactions made by all their Secondary users.

![ReportInquiry](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/64805c80-cc59-48e1-aa3e-05fd8b77e63e)

### Understanding the reports

The horizontal sections of the report divide it on the basis of payment groups - salaries, support and assets. Each section is further divided by the subtype of the payments.</br>

![ReportExcel - Rows](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/4b389696-ee20-4043-93bf-9653e476f61a)

The vertical sections (columns) present the following information:
<ul>
  <li>
    Column 1 - The budget limits for all payment groups. 
  </li>
  <li>
    Column 2 - The total expenses by payment groups.
  </li>
  <li>
    Column 3 - The sum of all bank payments.
  </li>
  <li>
    Column 4 - The sum of all cash payments.
  </li>
  <li>
    Column 5 - The sum of all transfer payments. A transfer payment is a payment back to the government budget. Social security payments and taxes are considered transfer payments.
  </li>
</ul>

![ReportExcel - Cols](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/44e6005a-2e75-4f3a-9613-fd5933614d36)
<a href="#table-of-contents">Back to index table</a>
## Administration pages
### Application settings page
The Administrator can change many of the global application settings like the life of different types of assets, amortization and residual value percentages, minimum wage, tax and insurance rates.

![AdminSettings](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/d6ce2344-7f43-410d-b807-ee20a92bc000)

### Delete reports page

The Administrator can also delete any report, both individual and consolidated, which has been saved and made public by the Primary and Secondary users.

![AdminReports](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/1de17eb6-4b62-4b97-9760-eae66d7f5da9)
<a href="#table-of-contents">Back to index table</a>
## Error page
If the application cannot process a request properly or the user engages in parameter tampering a simple error page will be displayed with a custom message, describing the cause of the error.

![ErrorBudget](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/1f42172c-cd88-4e89-9e75-95536a78e226)
![ErrorAsset](https://github.com/AlexanderRVulchev/Payments-Budget-System/assets/106471266/cc680465-8ecc-4d40-a328-c7830223a2eb)

<a href="#table-of-contents">Back to index table</a>
