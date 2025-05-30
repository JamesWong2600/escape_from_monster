"""
URL configuration for myproject project.

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/5.2/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path
from myapp import views as myapp_views
import myapp.views

urlpatterns = [
    path('admin/', admin.site.urls),
    path('register/', myapp.views.register, name='register'),
    path('login/', myapp.views.login, name='login'),
    path('get_player_scores/', myapp_views.get_player_scores, name='get_player_scores'),
    path('update_player_scores/', myapp_views.update_player_scores, name='update_player_scores'),
]
