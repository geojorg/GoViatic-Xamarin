using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("Type", "type")]
    public class TermsViewModel : BaseViewModel
    {
        private string _termsIcon;
        private string _termsTittle;
        private string _termsExplanation;
        private string _termsTittle1;
        private string _termsTittle2;
        private string _termsTittle3;
        private string _termsSub1;
        private string _termsSub2;
        private string _termsSub3;

        public string TermsIcon
        {
            get { return _termsIcon; }
            set { SetProperty(ref _termsIcon, value); }
        }
        public string TermsTittle
        {
            get { return _termsTittle; }
            set { SetProperty(ref _termsTittle, value); }
        }
        public string TermsExplanation
        {
            get { return _termsExplanation; }
            set { SetProperty(ref _termsExplanation, value); }
        }
        public string TermsTittle1
        {
            get { return _termsTittle1; }
            set { SetProperty(ref _termsTittle1, value); }
        }
        public string TermsTittle2
        {
            get { return _termsTittle2; }
            set { SetProperty(ref _termsTittle2, value); }
        }
        public string TermsTittle3
        {
            get { return _termsTittle3; }
            set { SetProperty(ref _termsTittle3, value); }
        }
        public string TermsSub1
        {
            get { return _termsSub1; }
            set { SetProperty(ref _termsSub1, value); }
        }
        public string TermsSub2
        {
            get { return _termsSub2; }
            set { SetProperty(ref _termsSub2, value); }
        }
        public string TermsSub3
        {
            get { return _termsSub3; }
            set { SetProperty(ref _termsSub3, value); }
        }

        public string Type
        {
            set 
            {
                var pagetype = value;
                if (pagetype == "privacy")
                {
                    TermsIcon = "ic_privacy";
                    TermsTittle = "GoViactic Privacy Policy";
                    TermsExplanation = "You should always know what data we collect from you and how we use it, and that you should have meaningful control over both. We want to empower you to make the best decisions about the information that you share with us. That's the basic purpose of this Privacy Policy";
                    TermsTittle1 = "Basic Account Information";
                    TermsTittle2 = "Public Information";
                    TermsTittle3 = "Contact Information";

                    TermsSub1 = "If you do choose to create an account, you must provide us with some personal data. On GoViatic this includes a display name, password, email address and the company you work for. Your display name and username are always private, but you can use either your real name or a pseudonym.";
                    TermsSub2 = "You are responsible for the information you provide through our services, and you should think carefully about what you make public, especially if it is sensitive information for you or your company.";
                    TermsSub3 = "We use your contact information, such as your email address to authenticate your account and keep it secure, and to help prevent spam, fraud, and abuse. GoViatic also uses your contact information to market to you as your country's laws allow. You can use your settings to control notifications you receive from GoViatic.";
                }
                else
                {
                    TermsIcon = "ic_goviatic";
                    TermsTittle = "GoViactic Terms of Service";
                    TermsExplanation = "These Terms of Service govern your access to and use of our services, including our website, APIs, email notifications, applications, buttons, widgets, ads, commerce services, and our other covered services. By using the Services you agree to be bound by these Terms.";
                    TermsTittle1 = "Who May Use the Services";
                    TermsTittle2 = "Content on the Services";
                    TermsTittle3 = "Your Account";

                    TermsSub1 = "You may use the Services only if you agree to form a binding contract with GoViatic and are not a person barred from receiving services under the laws of the applicable jurisdiction. In any case, you must be at least 16 years old. If you are accepting these Terms and using the Services on behalf of a company, organization, government, or other legal entity, you represent and warrant that you are authorized to do so.";
                    TermsSub2 = "You are responsible for your use of the Services and for any Content you provide, including compliance with applicable laws, rules, and regulations. You should only provide Content that you are comfortable sharing with others.";
                    TermsSub3 = "You may need to create an account to use some of our Services. You are responsible for safeguarding your account, so use a strong password and limit its use to this account. We cannot and will not be liable for any loss or damage arising from your failure to comply with the above";
                }
            }
        }
    }
}
