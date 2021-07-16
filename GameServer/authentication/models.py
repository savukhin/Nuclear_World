from django.db import models
from django.contrib.auth.models import User

# Create your models here.
class CustomUser(models.Model):
    user = models.OneToOneField(verbose_name="Реальный пользователь", to=User, on_delete=models.CASCADE,
                                related_name="customUser")
