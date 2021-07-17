from django.urls import path

from . import views

urlpatterns = [
    path('getNewUsersInfo', views.getNewUsersInfo),
]