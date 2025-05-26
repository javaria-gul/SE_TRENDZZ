using System.Web.Mvc;
using SE_TRENDZZ.Models;  // Required for TeamMember

namespace SE_TRENDZZ.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact(string name)
        {
            var member = new TeamMember();

            if (name == "Maria")
            {
                member.Name = "Maria Khan";
                member.Email = "mariakhushk2@gmail.com";
                member.PhoneNumber = "0319-3886208"; // Add this line
                member.GitHub = "https://github.com/maria-khan-khushk";
                member.LinkedIn = "https://linkedin.com/in/maria-khan-khushk-742bb22a3/";
                member.Instagram = "https://instagram.com/maria_khan_khushk";
                member.Description = "I'm an undergraduate Software Engineering student at Bahria University Karachi Campus, passionate about creating intuitive and visually engaging user experiences. My primary focus is on front-end development, where I’ve built a strong foundation in modern web technologies, UI/UX principles, and responsive design.\r\n\r\nWhile I also have exposure to backend development, my current goal is to deepen my expertise in front-end tools and frameworks to craft dynamic, user-centered applications. I enjoy translating ideas into clean, functional interfaces and continuously strive to enhance both my technical and creative skills.";
            }
            else if (name == "Javaria")
            {
                member.Name = "Javaria Gul";
                member.Email = "javaria@example.com";
                member.PhoneNumber = "0319-3886208"; // Add this line
                member.GitHub = "https://github.com/javaria-gul";
                member.LinkedIn = "https://www.linkedin.com/in/javariagul-developer/";
                member.Instagram = "https://www.instagram.com/javaria._.kh/";
                member.Description = "A results-driven Backend Developer with a strong understanding of server-side logic, database management, and application performance optimization. Skilled in building scalable APIs, managing backend services, and ensuring robust integration with front-end systems. Currently pursuing a Bachelor's degree in Software Engineering at Bahria University Karachi Campus, with hands-on experience in both academic and project-based environments. Passionate about writing clean, maintainable code and continuously learning new technologies to enhance backend efficiency and security.";
            }
            else if (name == "Fatima")
            {
                member.Name = "Fatima Ilyas";
                member.Email = "fatimailyas807@gmail.com";
                member.PhoneNumber = "03308674481"; // Add this line
                member.GitHub = "https://github.com/fatimaabc";
                member.LinkedIn = "https://www.linkedin.com/in/fatima-ilyas-6b980a362/";
                member.Instagram = "https://www.instagram.com/fatimailyas_";
                member.Description = "A detail-oriented Quality Assurance Engineer responsible for identifying bugs, validating features, and maintaining the overall quality of the application through rigorous manual and automated testing processes. Ensures that all functionalities meet defined standards and contribute to a seamless user experience. Currently pursuing a Bachelor's degree in Software Engineering at Bahria University Karachi Campus, with a strong foundation in software development life cycle, defect tracking, and continuous improvement practices.";
            }
            else if (name == "Ayesha")
            {
                member.Name = "Ayesha Ashfaq";
                member.Email = "ayeshaashfaq.a@icloud.com";
                member.PhoneNumber = "0301-2077338"; // Add this line
                member.GitHub = "https://github.com/ayeshaashfaq355";
                member.LinkedIn = "https://www.linkedin.com/in/ayesha-ashfaq-b879a2367/";
                member.Instagram = "https://www.instagram.com/ayeshaashfaq84/";
                member.Description = "An enthusiastic and solution-driven Software Engineering undergraduate at Bahria University Karachi Campus, experienced in troubleshooting and resolving code issues, improving system stability, and optimizing overall application performance. Adept at identifying root causes of technical problems and implementing efficient backend solutions to ensure smooth and reliable system functionality. Focused on enhancing code efficiency, reducing bottlenecks, and maintaining robust server-side logic for scalable web applications.";
            }

            return View(member);


        }

        [HttpPost]
        public ActionResult SendMessage(string ToName, string SenderName, string SenderEmail, string Message)
        {
            TempData["MsgSent"] = $"Message sent to {ToName} by {SenderName} ({SenderEmail}): {Message}";
            return RedirectToAction("About");
        }
    }
}
