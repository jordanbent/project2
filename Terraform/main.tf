terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 2.46.0"
    }
  }
}

provider "azurerm" {
  features {}
}

variable "project_name" {
  default = "jb"

}

locals {
  name_location = {
    "Service1" = "uksouth"
    "Service2" = "uksouth"
  }
}

locals {
  nameMerge     = "MergeService"
  locationMerge = "uksouth"
}

locals {
  nameFront     = "FrontEnd"
  locationFront = "uksouth"
}

resource "azurerm_resource_group" "main" {
  name     = "${var.project_name}-project"
  location = "uksouth"
}

resource "azurerm_app_service_plan" "plan" {
  name                = "${var.project_name}-plan"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name

  sku {
    tier = "Standard"
    size = "S1"
  }
}

#Service1 and Service2
resource "azurerm_app_service" "appservice" {

  for_each = local.name_location

  name                = "${var.project_name}-${each.key}-app"
  location            = each.value
  resource_group_name = azurerm_resource_group.main.name
  app_service_plan_id = azurerm_app_service_plan.plan.id
  depends_on          = [azurerm_app_service_plan.plan]

  lifecycle {
    prevent_destroy = true
  }
}

#Merge Service
resource "azurerm_app_service" "appserviceM" {

  name                = "${var.project_name}-${local.nameMerge}-app"
  location            = local.locationMerge
  resource_group_name = azurerm_resource_group.main.name
  app_service_plan_id = azurerm_app_service_plan.plan.id
  depends_on          = [azurerm_app_service_plan.plan]

  app_settings = {
    "colourServiceURL" = "https://jb-service1-app.azurewebsites.net"
    "fruitServiceURL"  = "https://jb-service2-app.azurewebsites.net"
  }

  lifecycle {
    prevent_destroy = true
  }
}

#~FrontEnd
resource "azurerm_app_service" "appserviceFE" {

  name                = "${var.project_name}-${local.nameFront}-app"
  location            = local.locationFront
  resource_group_name = azurerm_resource_group.main.name
  app_service_plan_id = azurerm_app_service_plan.plan.id
  depends_on          = [azurerm_app_service_plan.plan]

  app_settings = {
    "mergeServiceURL" = "https://jb-mergeservice-app.azurewebsites.net"
  }

  lifecycle {
    prevent_destroy = true
  }
}
