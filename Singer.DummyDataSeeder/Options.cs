using CommandLine;

namespace Singer.DummyDataSeeder
{
    internal class Options
    {
        private bool _generateCareUsers;
        private bool _generateLegalGuardians;
        private bool _generateAdmins;
        private bool _generateEvents;
        private bool _linkCareUserAndLegalGuardians;


        [Option('u', "url", HelpText = "The base url of the api"
#if DEBUG
            , Default = "https://localhost:5001"
#else
            , Required = true
#endif
            )]
        public string Url { get; set; }

        [Option('c', "careusers", HelpText = "Generate care users", Default = false)]
        public bool GenerateCareUsers
        {
            get => _generateCareUsers || GenerateAll || _linkCareUserAndLegalGuardians;
            set => _generateCareUsers = value;
        }

        [Option('l', "legalguardians", HelpText = "Generate legal guardian users", Default = false)]
        public bool GenerateLegalGuardians
        {
            get => _generateLegalGuardians || GenerateAll || _linkCareUserAndLegalGuardians;
            set => _generateLegalGuardians = value;
        }

        [Option('e', "events", HelpText = "Generate events", Default = false)]
        public bool GenerateAdmins
        {
            get => _generateAdmins || GenerateAll;
            set => _generateAdmins = value;
        }

        [Option('e', "events", HelpText = "Generate events", Default = false)]
        public bool GenerateEvents
        {
            get => _generateEvents || GenerateAll;
            set => _generateEvents = value;
        }

        [Option("link", HelpText = "Link the care users to the legal guardians. (This option automatically generates care users and legal guardians.)")]
        public bool LinkCareUsersAndLegalGuardians
        {
            get => _linkCareUserAndLegalGuardians || GenerateAll;
            set => _linkCareUserAndLegalGuardians = value;
        }

        [Option('a', "all", HelpText = "Generate all and link.", Default = false)]
        public bool GenerateAll { get; set; }

        internal bool GenerateNothing => !(GenerateAdmins || GenerateLegalGuardians || GenerateCareUsers || GenerateEvents);
    }
}
