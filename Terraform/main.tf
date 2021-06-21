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
    "FrontEnd"     = "uksouth"
    "MergeService" = "uksouth"
    "Service1"     = "uksouth"
    "Service2"     = "uksouth"
  }
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
