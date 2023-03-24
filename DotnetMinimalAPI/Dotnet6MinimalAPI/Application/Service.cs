namespace Dotnet6MinimalAPI.Application
{
    public class Service : IService
    {
        private readonly XDocument _xDocument;
        private readonly IOptions<KontofonMonitorConfig> _options;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Service(IOptions<KontofonMonitorConfig> options)
        {
            _options = options;
            if (_options.Value == null)
            {
                logger.Info("Kontofon monitor log file path is empty.");
                throw new GeneralApplicationException("Kontofon monitor log file path is empty.");
            }
            try
            {
                var xmlFilePath = _options.Value.KontofonMonitorLogFilepath;
                _xDocument = XDocument.Load(xmlFilePath);
            }
            catch (Exception ex)
            {
                logger.Info($"Failed to load kontofon monitor log file: {ex}");
                throw new GeneralApplicationException("Failed to load kontofon monitor log file.", ex);
            }
        }

        public Wrapper.Result<List<Tjenester>> GetServices()
        {
            try
            {
                var tjenester = _xDocument.Descendants("Tjeneste")
                               .Select(tjeneste => new Tjenester
                               {
                                   Navn = tjeneste?.Attribute("navn")?.Value ?? null,
                                   Status = tjeneste?.Attribute("status")?.Value ?? null,
                               })
                               .ToList();

                if (tjenester == null || !tjenester.Any())
                {
                    logger.Info("Services data not found in kontofon monitor log file.");
                    return Wrapper.Result<List<Tjenester>>.Fail("Services data not found in kontofon monitor log file.");
                }
                var data = tjenester.ToJson();

                logger.Info($"Services fetch from kontofon monitor log file: {tjenester.ToJson()}");
                return Wrapper.Result<List<Tjenester>>.Success(tjenester);
            }
            catch (Exception ex)
            {
                logger.Info($"Failed to read Services data from kontofon monitor log file: {ex}");
                throw new GeneralApplicationException("Failed to read Services data from kontofon monitor log file.", ex);
            }
        }

        public Wrapper.Result<List<Models.Tjeneste>> GetServicesWithDetails()
        {
            try
            {
                var tjenester = _xDocument.Descendants("Tjeneste").Select(tjeneste => new Models.Tjeneste
                {
                    Navn = tjeneste?.Attribute("navn")?.Value ?? null,
                    Status = tjeneste?.Attribute("status")?.Value ?? null,
                    funksjoner = tjeneste?.Descendants("Funksjon")?.Select(fun => new funksjon
                    {
                        Id = fun?.Attribute("id")?.Value ?? null,
                        Type = fun?.Attribute("type")?.Value ?? null,
                        System = fun?.Element("system")?.Value ?? null,
                        Status = fun?.Element("status")?.Value ?? null,
                        StatusKode = fun?.Element("statuskode")?.Value ?? null,
                        Tidspunkt = fun?.Element("tidspunkt")?.Value ?? null,
                        FeilTekst = fun?.Element("feiltekst")?.Value ?? null
                    }).ToList()
                }).ToList();

                if (tjenester == null || !tjenester.Any())
                {
                    logger.Info("Services details data not found in kontofon monitor log file.");
                    return Wrapper.Result<List<Models.Tjeneste>>.Fail("Services details data not found in kontofon monitor log file.");
                }
                logger.Info($"Services details fetch from kontofon monitor log file: {tjenester.ToJson()}");
                return Wrapper.Result<List<Models.Tjeneste>>.Success((List<Models.Tjeneste>)tjenester, (int)tjenester.Count, "Data retrived succefully from kontofon monitor log file!");
            }
            catch (Exception ex)
            {
                logger.Info($"Failed to read Services Details data from kontofon monitor log file: {ex}");
                throw new GeneralApplicationException("Failed to read Services Details data from kontofon monitor log file.", ex);
            }
        }
    }
}
