﻿namespace KontofonMonitor.API.Application
{
    public interface IService
    {
        Wrapper.Result<List<Models.Tjenester>> GetServices();
        Wrapper.Result<List<Models.Tjeneste>> GetServicesWithDetails();
    }
}
