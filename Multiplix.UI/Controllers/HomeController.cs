﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Models;
using MercadoPago;
using MercadoPago.Resources;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Common;
using Multiplix.UI.Utils;
using Multiplix.Domain.Enums;

namespace Multiplix.UI.Controllers
{
    public class HomeController : Controller
    {       
        private IServicePatrocinador _servicePatrocinador;
        private IServiceParceiro _serviceParceiro;
        private IServiceUsuario _serviceUsuario;

        public HomeController(IServicePatrocinador servicePatrocinador, IServiceUsuario serviceUsuario, IServiceParceiro serviceParceiro)
        {
            _servicePatrocinador = servicePatrocinador;
            _serviceUsuario = serviceUsuario;
            _serviceParceiro = serviceParceiro;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessarPagamento()
        {
            // Configura credenciais
            MercadoPago.SDK.AccessToken = "TEST-874307809966259-101820-90e98a29692cbee8f176ed57f076e283-19846703";

            // Cria um objeto de preferência
            Preference preference = new Preference();
          
            // Cria um item na preferência
            preference.Items.Add(
              new Item()
              {
                  Title = "Meu produto",
                  Quantity = 1,
                  CurrencyId = CurrencyId.BRL,
                  UnitPrice = (decimal)75.56                  
              }
            );
            preference.Save();

            return View();

        }

        public IActionResult Dashboard()
        {

            var usuarioLogado = UsuarioUtils.GetUsuarioLogado(HttpContext, _serviceUsuario);

            if (usuarioLogado.TipoUsuario == (int)ETipoUsuario.PARCEIRO)
            {
                return Redirect("/Home/DashboardParceiro");
            }

            var associado = _servicePatrocinador.Buscar(x => x.Usuario.UsuarioId == usuarioLogado.UsuarioId).FirstOrDefault();

            var totalIndividual = _servicePatrocinador.GetGanhosIndividual(DateTime.Now.Month, associado.Id);
            var totalRede = _servicePatrocinador.GetGanhosRede(DateTime.Now.Month, associado.Id);
            var somaBonus = _servicePatrocinador.GetBonusPorMes(DateTime.Now.Month, associado.Id);

            var associadoRede = _servicePatrocinador.GetRedeAssociado(associado.Id);
            ViewBag.QTDRede = associadoRede.Count();
            ViewBag.TotalARecebeber = totalIndividual + totalRede + somaBonus;
            ViewBag.QtdCompras = associado.Compras.Where(x => x.Data.Month == DateTime.Now.Month).Sum(x=>x.Valor);
            ViewBag.NumeroCartao = associado.IdCarteira.ToString();
            return View();           
        }

        public IActionResult DashboardParceiro()
        {
            var usuarioLogado = UsuarioUtils.GetUsuarioLogado(HttpContext, _serviceUsuario);
            var parceiro = _serviceParceiro.Buscar(x => x.Usuario.UsuarioId == usuarioLogado.UsuarioId).FirstOrDefault();

            ViewBag.QTDVendas = parceiro.Compras.Where(x => x.Data.Month == DateTime.Now.Month).Count();
            ViewBag.TotalVendas = parceiro.Compras.Where(x => x.Data.Month == DateTime.Now.Month).Sum(x => x.Valor);
            ViewBag.TotalRepasse = parceiro.Compras.Where(x => x.Data.Month == DateTime.Now.Month).Sum(x => x.CompraItems.Sum(y => y.ValorRepasse));
            
            return View();
        }

        public IActionResult SalvarTeste()
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO();
            usuarioDTO.PatrocinadorId = 0;
            usuarioDTO.Nome = "Esdras";
            usuarioDTO.Email = "esdras.valentim@yahoo.com.br";
            usuarioDTO.Login = "esdras";
            usuarioDTO.Senha = "123";
            usuarioDTO.Liberado = true;
            usuarioDTO.SuperUser = 0;

            var result = _servicePatrocinador.SalvarAssociadoSemConvite(usuarioDTO);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
