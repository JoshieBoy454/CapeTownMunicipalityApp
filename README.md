# Cape Town Municipality App 

---

[![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/)
[![Razor Pages](https://img.shields.io/badge/Razor_Pages-68217A?logo=razor&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/)
[![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=black)](https://developer.mozilla.org/en-US/docs/Web/JavaScript)
[![HTML5](https://img.shields.io/badge/HTML5-E34F26?logo=html5&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/Guide/HTML/HTML5)
[![CSS3](https://img.shields.io/badge/CSS3-1572B6?logo=css3&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/CSS)
[![Entity Framework](https://img.shields.io/badge/Entity_Framework-68217A?logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?logo=visual-studio&logoColor=white)](https://visualstudio.microsoft.com/)

A web application designed to enhance user engagement for municipal services in a South African context. The app allows citizens to report issues, track requests, and access municipal services efficiently.

---

## Overview

The Municipal Services User Engagement App is designed to:

* Allow residents to submit service requests and report issues.
* Track the progress of submitted requests.
* Recieve information about events.
* Enhance transparency and communication between the municipality and citizens.
---

## Features

1. **Report Issues**

   * Users can submit requests with descriptions, images, and locations.
   * Requests are categorized.

2. **Citizen Engagement**

   * A progress bar has been added to engage users.
   * Multi-Language support.
  
3. **Events and Announcements**

   * Users can view Events and Announcements posted.
   * Users can filter for specific events based on Category, Type, and Date.
   * Frequently visited categories are captured and similar categories are displayed inthe **Recommedned for You** section.

---

## Youtube Video Link

[![YouTube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://youtu.be/PakiE3Z3yxc)

---

## GitHub Link

[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/JoshieBoy454/CapeTownMunicipalityApp.git)

---

## Prerequisites

[![Visual Studio](https://img.shields.io/badge/Visual_Studio-IDE-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white)](https://visualstudio.microsoft.com/)

* Internet connection for live updates and service requests

---

## Setup Instructions

1. **Clone via Git:**
   - Open your Git Bash or terminal.
   - Run the following command to clone the repository:
     ```bash
     git clone https://github.com/JoshieBoy454/CapeTownMunicipalityApp.git
     ```

2. **Open the project:**
   - Open Visual Studio.
   - Click on `File` -> `Open` -> `Project/Solution`.
   - Navigate to the cloned folder and select the `.sln` file to open the project.
  
3. **Restore Dependencies:**
   - In Visual Studio, go to `Tools` -> `NuGet Package Manager` -> `Manage NuGet Packages for Solution`.
   - Click on `Restore` to restore the necessary packages.

4. **Build the Project:**
   - Press `Ctrl+Shift+B` to build the project.

5. **Run the Application:**
   - Press `F5` to run the application. Visual Studio will start the application, and you should see the application's main      window.
     
---

## Architecture

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)

[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-web--framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/)

[![Razor Pages](https://img.shields.io/badge/Razor_Pages-CSHTML-68217A?style=for-the-badge)](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/)

[![JavaScript](https://img.shields.io/badge/JavaScript-dynamic--scripts-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)](https://developer.mozilla.org/en-US/docs/Web/JavaScript)

[![HTML5](https://img.shields.io/badge/HTML5-markup-E34F26?style=for-the-badge&logo=html5&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/Guide/HTML/HTML5)

[![CSS3](https://img.shields.io/badge/CSS3-styling-1572B6?style=for-the-badge&logo=css3&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/CSS)

[![Entity Framework](https://img.shields.io/badge/Entity_Framework-ORM-68217A?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)

[![SQL Server](https://img.shields.io/badge/SQL_Server-database-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)

[![Bootstrap](https://img.shields.io/badge/Bootstrap-frontend-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)

[![Visual Studio](https://img.shields.io/badge/Visual_Studio-IDE-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white)](https://visualstudio.microsoft.com/)

---

## Non-Functional Requirements

* **Performance**: Fast load times and responsive UI.
* **Security**: User authentication and secure storage of sensitive information.
* **Scalability**: Can handle increasing numbers of users and requests.
* **Availability**: Offline access for cached requests and local storage.
* **Usability**: Simple interface for users of all tech literacy levels.

---

## Changes

* Implemented Services page that allows users to track their submitted reports and service requests, ordered in a list form.
* Added a search/filter feature to search specific revice requests.
* Created unique identifiers (tracking code) that users can use to track their specific requests.
* Created a modal to display specific information on service requests.
* Implmented a min heap binary tree, binary search tree, and status graph.
* Minor fixes to ui and redirection logic

---

## References

* listCodeBeauty (2025). [online] Youtu.be. Available at: https://youtu.be/-StYr9wILqo?si=Fv-mWDv_bcmqK8Pz [Accessed 10 Sep. 2025].
* Contentsquare.com. (2025). 8 Actionable User Engagement Strategies For Higher Retention. [online] Available at: https://contentsquare.com/guides/user-engagement/strategies/ [Accessed 10 Sep. 2025].
* dotnet-bot (2025). Carousel Class (Microsoft.Toolkit.Uwp.UI.Controls). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/microsoft.toolkit.uwp.ui.controls.carousel?view=win-comm-toolkit-dotnet-7.1 [Accessed 11 Sep. 2025].
* GeeksforGeeks (2019). C# LinkedList. [online] GeeksforGeeks. Available at: https://www.geeksforgeeks.org/c-sharp/linked-list-implementation-in-c-sharp/ [Accessed 11 Sep. 2025].
* Globalapptesting.com. (2023). Top 30+ mobile app development statistics and facts. [online] Available at: https://www.globalapptesting.com/blog/mobile-app-development-statistics-and-facts [Accessed 10 Sep. 2025].
* Microsoft.com. (2023). Multi-Language MVC Form - Microsoft Q&A. [online] Available at: https://learn.microsoft.com/en-us/answers/questions/1179879/multi-language-mvc-form [Accessed 11 Sep. 2025].
* Segment (2021). The State of Personalization 2021 From nice-to-have to necessity for intelligent customer engagement. [online] Available at: https://gopages.segment.com/rs/667-MPQ-382/images/State-of-personalization-report_reduced.pdf.
* Sharma, P. (2024). 22 Proven Strategies to Improve App Engagement in 2025 | VWO. [online] Blog. Available at: https://vwo.com/blog/improve-app-engagement/ [Accessed 10 Sep. 2025].
* Sister Cities. (n.d.). South African Culture – Sister Cities International (SCI). [online] Available at: https://sistercities.org/africa-summit/south-african-culture.tdykstra (2025a). Session in ASP.NET Core. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-9.0 [Accessed 11 Sep. 2025].
* tdykstra (2025b). Session in ASP.NET Core. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-9.0 [Accessed 11 Sep. 2025].
* Team, S. (2021). The Countries With the Most Official Languages | Ad Astra. [online] Ad-astrainc.com. Available at: https://ad-astrainc.com/blog/the-countries-with-the-most-official-languages-and-how-they-translate [Accessed 10 Sep. 2025].
* TutorialsEU - C# (2025). [online] Youtu.be. Available at: https://youtu.be/DKaN8yjqHsw?si=EBL0icKsYP_qeH4w [Accessed 11 Sep. 2025].
* ChatGPT. (2024). ChatGPT. [online] Available at: https://chatgpt.com/ [Accessed 15 Oct. 2025].
* Reference listGeeksforGeeks (2023). Introduction to MinHeap. [online] GeeksforGeeks. Available at: https://www.geeksforgeeks.org/dsa/introduction-to-min-heap-data-structure/ [Accessed 12 Nov. 2025].
* GeeksforGeeks (2024). Binary Search Tree. [online] GeeksforGeeks. Available at: https://www.geeksforgeeks.org/dsa/binary-search-tree-data-structure/ [Accessed 12 Nov. 2025].

---
