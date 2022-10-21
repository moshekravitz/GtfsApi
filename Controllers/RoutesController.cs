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
    public class stopTimesController : ControllerBase
    {
        public IStopTimesRepo repository;
        public stopTimesController(IStopTimesRepo repository)
        {
            this.repository = repository;
        }

        [HttpGet("id")]
        [ApiKey]
        public async Task<ActionResult<StopTimesListDto>> GetSingleStopTimes(int routeId)
        {
            var result = await repository.GetSingleAsync(routeId);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.AsDto());
        }

        [HttpPost("Admin")]
        [ApiKeyAdmin]
        public async Task CreateStopTimesListList(List<StopTimesList> stopTLList)
        {
            await repository.CreateListAsync(stopTLList);
        }

        [HttpPut("Admin")]
        [ApiKeyAdmin]
        public async Task UpdateStopTimesListList(List<StopTimesList> stopTLList) 
        {
            await repository.UpdateListAsync(stopTLList);
        }

        [HttpDelete("Admin")]
        [ApiKeyAdmin]
        public async Task DeleteManyStopTimesLists(List<StopTimesList> stopTLList)
        {
            await repository.DeleteManyAsync(stopTLList);
        }

        [HttpDelete("all/Admin")]
        [ApiKeyAdmin]
        public async Task DeleteAllStopTimesLists()
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


    [Route("api/[controller]")]
    [ApiController]
    public class RouteToDateController : ControllerBase
    {
        public IRouteToDateRepo repository;
        public RouteToDateController(IRouteToDateRepo repository)
        {
            this.repository = repository;
        }

        [HttpGet("idList")]
        [ApiKey]
        public async Task<ActionResult<RouteToDate>> Get(int routeId)
        {
            RouteToDate result = await repository.GetSingleAsync(routeId);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.AsDto());
        }

        [HttpPost("Admin")]
        [ApiKeyAdmin]
        public async Task CreateList(List<RouteToDate> routeToDateList)
        {
            await repository.CreateListAsync(routeToDateList);
        }


        [HttpPut("Admin")]
        [ApiKeyAdmin]
        public async Task UpdateStopInfoList(List<RouteToDate> routeToDateList) 
        {
            await repository.UpdateListAsync(routeToDateList);
        }

        [HttpDelete("Admin")]
        [ApiKeyAdmin]
        public async Task DeleteManyStopInfo(List<RouteToDate> routeToDateList)
        {
            await repository.DeleteManyAsync(routeToDateList);
        }

        [HttpDelete("all/Admin")]
        [ApiKeyAdmin]
        public async Task DeleteAll()
        {
            await repository.DeleteAllAsync();
        }
    }
}
