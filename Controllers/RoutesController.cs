using Catalog.Attributes;
using Catalog.repositories;
using GtfsApi.Dots;
using GtfsApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        public IRoutesRepo repository;
        public RoutesController(IRoutesRepo repository)
        {
            this.repository = repository;
        }

        // GET: api/<RoutesController>

        [HttpGet]
        [ApiKey]
        public async Task<ActionResult<IEnumerable<RouteDto>>> getAllRoutes()
        {
            var routeList = (await repository.GetAllAsync()).ConvertAll(new Converter<Routes, RouteDto>(Extensions.AsDto));
            if (routeList is null)
            {
                return NotFound();
            }
            return Ok(routeList);
        }

        [HttpPost("Admin")]
        [ApiKeyAdmin]
        public async Task CreateRoutesList(List<Routes> routesList)
        {
            await repository.CreateListAsync(routesList);
        }

        [HttpPut("Admin")]
        [ApiKeyAdmin]
        public async Task UpdateRoutesList(List<Routes> routesList) 
        {
            await repository.UpdateListAsync(routesList);
        }

        [HttpDelete("Admin")]
        [ApiKeyAdmin]
        public async Task DeleteManyRoutes(List<Routes> routesList)
        {
            await repository.DeleteManyAsync(routesList);
        }

        [HttpDelete("all/Admin")]
        [ApiKeyAdmin]
        public async Task DeleteAllExtendedRoutes()
        {
            await repository.DeleteAllAsync();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ExtendedRoutesController : ControllerBase
    {
        public IExtendedRoutesRepo repository;
        public ExtendedRoutesController(IExtendedRoutesRepo repository)
        {
            this.repository = repository;
        }

        [HttpGet("id")]
        [ApiKey]
        public async Task<ActionResult<ExtendedRouteDto>> GetSingleRoute(int routeId)
        {
            var route = await repository.GetSingleAsync(routeId);
            if (route is null)
            {
                return NotFound();
            }

            return Ok(route.AsDto());
        }

        [HttpPost("Admin")]
        [ApiKeyAdmin]
        public async Task CreateExtendedRoutesList(List<ExtendedRoutes> routesList)
        {
            await repository.CreateListAsync(routesList);
        }


        [HttpPut("Admin")]
        [ApiKeyAdmin]
        public async Task UpdateExtendedRoutesList(List<ExtendedRoutes> routesList) 
        {
            await repository.UpdateListAsync(routesList);
        }

        [HttpDelete("Admin")]
        [ApiKeyAdmin]
        public async Task DeleteManyExtendedRoutes(List<ExtendedRoutes> routesList)
        {
            await repository.DeleteManyAsync(routesList);
        }

        [HttpDelete("all/Admin")]
        [ApiKeyAdmin]
        public async Task DeleteAllExtendedRoutes()
        {
            await repository.DeleteAllAsync();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class stopInfoController : ControllerBase
    {
        public IStopInfoRepo repository;
        public stopInfoController(IStopInfoRepo repository)
        {
            this.repository = repository;
        }

        [HttpGet("idList")]
        [ApiKey]
        public async Task<ActionResult<IEnumerable<StopInfoDto>>> Get(List<int> stopIdL)
        {
            IEnumerable<StopInfo> result = await repository.GetListAsync(stopIdL);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.AsDto());
        }

        [HttpPost("Admin")]
        [ApiKeyAdmin]
        public async Task CreateList(List<StopInfo> stopInfoList)
        {
            await repository.CreateListAsync(stopInfoList);
        }

        [HttpPut("Admin")]
        [ApiKeyAdmin]
        public async Task UpdateStopInfoList(List<StopInfo> stopInfoList) 
        {
            await repository.UpdateListAsync(stopInfoList);
        }

        [HttpDelete("Admin")]
        [ApiKeyAdmin]
        public async Task DeleteManyStopInfo(List<StopInfo> stopInfoList)
        {
            await repository.DeleteManyAsync(stopInfoList);
        }

        [HttpDelete("all/Admin")]
        [ApiKeyAdmin]
        public async Task DeleteAll()
        {
            await repository.DeleteAllAsync();
        }
    }
}
