from django.urls import path

from . import views

urlpatterns = [
    path('getNewUsersInfo', views.getNewUsersInfo),
    path('getNewTransformsInfo', views.getNewTransformsInfo),

    path('updateTransform', views.updateTransform),
]