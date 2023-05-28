#!/bin/bash

sudo rm -r /var/deadman-switch-failed/mvp_web
sudo cp -r . /var/deadman-switch-failed/mvp_web

sudo chown -R http /var/deadman-switch-failed/mvp_web
sudo chgrp -R http /var/deadman-switch-failed/mvp_web
sudo chmod -R 777 /var/deadman-switch-failed/mvp_web
